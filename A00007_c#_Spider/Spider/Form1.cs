using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Spider
{
    public partial class Form1 : Form
    {
        const int FEATURE_DISABLE_NAVIGATION_SOUNDS = 21;
        const int SET_FEATURE_ON_PROCESS = 0x00000002;

        [DllImport("urlmon.dll")]
        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        static extern int CoInternetSetFeatureEnabled(
            int FeatureEntry,
            [MarshalAs(UnmanagedType.U4)] int dwFlags,
            bool fEnable);

        [DllImport("KERNEL32.DLL", EntryPoint = "SetProcessWorkingSetSize", SetLastError = true, CallingConvention = CallingConvention.StdCall)]
        internal static extern bool SetProcessWorkingSetSize(IntPtr pProcess, int dwMinimumWorkingSetSize, int dwMaximumWorkingSetSize);

        [DllImport("KERNEL32.DLL", EntryPoint = "GetCurrentProcess", SetLastError = true, CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr GetCurrentProcess();

        private List<Company> companyList;
        /// <summary>
        /// 目录结构
        /// data_时间
        ///     company
        ///         company.csv
        ///         product.csv
        ///         files 资源文件
        /// </summary>
        private string dataPath;
        private bool GetCompanyListDoen;
        private bool GetCompanyDetaildDoen;
        private bool GetProductListDoen;
        private bool GetProductDetaildDoen;
        private Company CurrentCompany;
        private Product CurrentProduct;
        private SpiderWebClient SpiderWebClient;
        private bool isRun;
        public Form1()
        {
            InitializeComponent();
            CoInternetSetFeatureEnabled(
           FEATURE_DISABLE_NAVIGATION_SOUNDS,
           SET_FEATURE_ON_PROCESS,
           true);
            lblState.Text = string.Empty;
            lblUrl.Text = string.Empty;
            Control.CheckForIllegalCrossThreadCalls = false;
        }
        private string GetListFilePath()
        {
            return txtDataPath.Text + "\\LIST.TXT";

        }
        private void SaveListFile()
        {
            string listPath = GetListFilePath();
            Common.WrtieCompanList(listPath, companyList);

        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //Thread thread = new Thread(() =>
                // {

                //     _webBrowser.Navigate("http://ie.icoa.cn/");
                //    return;
                button2.Enabled = false;
                button1.Enabled = false;
                button3.Enabled = false;
                button4.Enabled = true;
                Company.COMPANY_SUM = 0;
                Product.PRODUCT_SUM = 0;
                dataPath = string.Empty;
                GetCompanyListDoen = false;
                GetCompanyDetaildDoen = false;
                GetProductListDoen = false;
                GetProductDetaildDoen = false;
                CurrentCompany = null;
                CurrentProduct = null;
                isRun = true;
                SpiderWebClient = new SpiderWebClient();
                Stopwatch sw = Stopwatch.StartNew();
                SetAppState("开始抓取公司ID.....");
                string listPath = GetListFilePath();
                if (!File.Exists(listPath))
                {

                    companyList = new List<Company>();
                    //抓取公司列表
                    GetCompanyList();
                    wait(ref GetCompanyListDoen);
                }
                else
                {
                    companyList = Common.ReadCompanList(GetListFilePath());
                }
                SetAppState("抓取公司ID结束，总共抓取" + companyList.Count + "个公司ID");
                //公司分别处理
                GetCompanyALLInfo();
                //等待所有下载任务结束
                waitDownLoaded();
                SetAppState("结束!共读取" + Company.COMPANY_SUM + "家公司和产品" + Product.PRODUCT_SUM
                            + "个，总共耗时:" + (sw.ElapsedMilliseconds / 60 / 60) + "/分钟.");
                button1.Enabled = true;
                button2.Enabled = true;
                //  });
                //thread.SetApartmentState(ApartmentState.STA);
                //thread.Start();
            }
            catch (Exception ex)
            {
                SaveListFile();
                MessageBox.Show("1034 ERROR!\r\n" + ex.Message);

                //                throw;
            }

        }

        private void waitDownLoaded()
        {
            while (!SpiderWebClient.IsEmptyJobQueue())
            {
                Thread.Sleep(200);
                Application.DoEvents();
            }
        }

        private void wait(ref bool doen)
        {
            while (!doen)
            {
                Thread.Sleep(200);
                Application.DoEvents();
            }

        }
        private void SetAppState(string message)
        {

            lbAppState.Items.Add(message);
            lbAppState.TopIndex = lbAppState.Items.Count - (int)(lbAppState.Height / lbAppState.ItemHeight);

        }
        void GetCompanyList()
        {
            _webBrowser.DocumentCompleted += _webBrowser_Company_List_DocumentCompleted;
            _webBrowser.Navigate(UsedMachinerySite.COMPANY_LIST_URL);

        }

        private void _webBrowser_Company_List_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            try
            {
                HtmlDocument document = _webBrowser.Document;
                ClearMemory();
                if (document != null)
                {
                    HtmlElementCollection hl = document.GetElementsByTagName("A");
                    HtmlElement nextpage = null;
                    foreach (HtmlElement he in hl)
                    {
                        string href = he.GetAttribute("HREF");
                        if (href.StartsWith(UsedMachinerySite.COMPANY_DETAIL_URL))
                        {
                            //提取公司ID
                            string companyId = GetLastStringByUrl(href);
                            Company company = new Company();
                            company.Id = companyId;
                            company.Name = he.InnerText;
                            companyList.Add(company);
                            //System.Diagnostics.Debug.WriteLine(he.InnerText);
                        }
                        //查找下一页按钮
                        if (he.InnerText == UsedMachinerySite.NEXT_PAGE_TEXT)
                        {
                            nextpage = he;
                        }
                    }
                    //处理完了,切换页面
                    if (nextpage != null)
                    {

                        //#if !DEBUG
                        nextpage.InvokeMember("click");
                        return;
                        //#endif
                    }

                    //全部处理完成，改变程序状态
                    GetCompanyListDoen = true;
                }
            }
            catch (Exception ex)
            {
                SaveListFile();
                MessageBox.Show("1035 ERROR!\r\n" + ex.Message);
                // throw;
            }

        }

        void GetCompanyALLInfo()
        {
            try
            {
                //解绑事件
                _webBrowser.DocumentCompleted -= _webBrowser_Company_List_DocumentCompleted;

                string msg1 = "[{0}] 已抓取，跳过  {1}  公司数据处理....";
                //构建跟节点  data_日期
                if (string.IsNullOrEmpty(txtDataPath.Text))
                {
                    dataPath = Path.GetTempPath() + "Data_" + DateTime.Now.ToString("yyyyMMddHHmmssffff");

                }
                else
                {
                    dataPath = txtDataPath.Text;
                }
                if (!Directory.Exists(dataPath))
                {
                    Directory.CreateDirectory(dataPath);
                }

                foreach (Company company in companyList)
                {
                    //初始化状态
                    GetCompanyDetaildDoen = false;
                    GetProductListDoen = false;
                    GetProductDetaildDoen = false;
                    bool isCreated = false;
                    //创建公司目录
                    company.Folder = dataPath + "\\" + company.Name;
                    int cIndex = (1 + companyList.IndexOf(company));
                    if (!Directory.Exists(company.Folder))
                    {

                        if (!isRun)
                        {
                            //等待下载完成
                            SetAppState("程序即将停止，正在等待下载工作结束.");
                            waitDownLoaded();
                            SaveListFile();
                            button3.Enabled = true;
                            SetAppState("程序停止，如果继续请点击【继续】按钮");
                            wait(ref isRun);
                        }
                        Directory.CreateDirectory(company.Folder);
                        isCreated = true;
                    }
                    else
                    {
                        SetAppState(string.Format(msg1, cIndex, company.Name));
                        continue;
                    }
                    //if (company.IsDonen)
                    //{
                    //    SetAppState(string.Format(msg1, cIndex, company.Name));
                    //    continue;
                    //}
                    SetAppState("[" + cIndex + "] 开始抓取  " + company.Name + "  公司数据....");
                    //当前处理的公司对象
                    CurrentCompany = company;
                    //公司ID
                    string companyId = company.Id;
                    //抓取公司详细信息
                    lblState.Text = "开始抓取该公司详细信息....";
                    _webBrowser.DocumentCompleted -= _webBrowser_Product_Detail_DocumentCompleted;
                    _webBrowser.DocumentCompleted += _webBrowser_Company_Detail_DocumentCompleted;
                    _webBrowser.Navigate(UsedMachinerySite.COMPANY_DETAIL_URL + companyId);

                    wait(ref GetCompanyDetaildDoen);
                    lblState.Text = "开始抓取该公司所有产品ID....";
                    if (isCreated && CurrentCompany.Products.Count == 0)
                    {//从新抓取，并且没有产品记录。必须从新抓取

                        //抓取产品ID
                        _webBrowser.DocumentCompleted -= _webBrowser_Company_Detail_DocumentCompleted;
                        _webBrowser.DocumentCompleted += _webBrowser_Product_List_DocumentCompleted;
                        _webBrowser.Navigate(UsedMachinerySite.PRODUCT_LIST_URL + companyId);
                        wait(ref GetProductListDoen);
                    }
                    else
                    {
                        //非从新抓取，已有商品列表。不再抓取
                    }
                    lblState.Text = "共抓取商品ID" + CurrentCompany.Products.Count + "个";
                    SetAppState(lblState.Text);
                    //抓取产品详细信息 
                    _webBrowser.DocumentCompleted -= _webBrowser_Product_List_DocumentCompleted;
                    _webBrowser.DocumentCompleted += _webBrowser_Product_Detail_DocumentCompleted;
                    if (company.Products.Count == 0)
                    {
                        GetProductDetaildDoen = true;
                    }
                    else
                    {
                        Product product = null;
                        foreach (Product p in company.Products)
                        {
                            if (!p.IsDonen)
                            {
                                product = p; //处理第一个非完成的产品
                                break;
                            }

                        }
                        CurrentProduct = product;
                        _webBrowser.Navigate(UsedMachinerySite.PRODUCT_DETAIL_URL + product.Id);
                    }
                    //等待商品信息获取完成
                    wait(ref GetProductDetaildDoen);
                    //公司信息存盘
                    CurrentCompany.Print();
                    //产品信息存盘
                    CurrentCompany.PrintProducts();
                    CurrentCompany.IsDonen = true;
                    //    companyList.Remove(CurrentCompany);

                    ClearMemory();
                }

                //全部处理完毕
                System.Diagnostics.Process.Start(dataPath);
            }
            catch (Exception ex)
            {

                SaveListFile();
                MessageBox.Show("1038 ERROR!\r\n" + ex.Message);
                // throw;
            }
        }

        private static void ClearMemory()
        {
            IntPtr pHandle = GetCurrentProcess();
            SetProcessWorkingSetSize(pHandle, -1, -1);
        }

        private void _webBrowser_Product_Detail_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            try
            {
                //提取商品详细
                HtmlDocument document = _webBrowser.Document;
                ClearMemory();
                if (document != null)
                {
                    //存在多次触发问题，使用url进行验证
                    if (!document.Url.ToString().StartsWith(UsedMachinerySite.PRODUCT_DETAIL_URL + CurrentProduct.Id))
                    {
                        return;
                    }
                    int pIndex = CurrentCompany.Products.IndexOf(CurrentProduct);

                    lblState.Text = "[" + (pIndex + 1) + "]" + " 开始抓取该公司 " + CurrentProduct.Name + " 产品数据....";
                    HtmlElementCollection hl = document.GetElementsByTagName("TD");
                    CurrentProduct.StockCode = GetTextB(hl[0]);
                    //   CurrentProduct.Manufacturter = GetTextB(hl[3].FirstChild.FirstChild);
                    CurrentProduct.Manufacturter = GetTextB(hl[3]);
                    CurrentProduct.Model = GetTextB(hl[4]);
                    CurrentProduct.ModelYear = GetTextB(hl[5]);
                    CurrentProduct.AskingPrice = GetTextB(hl[6]);
                    CurrentProduct.MainSpecification = GetTextB(hl[7]);
                    CurrentProduct.Accessory = GetTextB(hl[8]);
                    CurrentProduct.Condition = GetTextB(hl[9]);
                    CurrentProduct.Size = GetTextB(hl[10]);
                    CurrentProduct.Weight = GetTextB(hl[11]);
                    CurrentProduct.QuotationConditions = GetTextB(hl[12]);
                    HtmlElement a = hl[12].FirstChild.FirstChild;
                    if (a == null)
                    {
                        CurrentProduct.InformationURL = string.Empty;
                    }
                    else
                    {
                        CurrentProduct.InformationURL = a.GetAttribute("src");
                    }
                    CurrentProduct.StockLocation = GetTextB(hl[14]);
                    CurrentProduct.StockLocationDetail = GetTextB(hl[15]);


                    hl = document.GetElementsByTagName("LI");
                    CurrentProduct.Category = GetTextB(hl[14]);


                    hl = document.GetElementsByTagName("P");
                    string text = GetTextB(hl[1]);
                    string[] textPart = text.Split(' ');
                    if (textPart.Length == 3)
                    {
                        text = textPart[1];
                        text = text.Replace(",", "");
                    }
                    CurrentProduct.AccessCount = Convert.ToInt32(text);


                    hl = document.GetElementsByTagName("IMAGE");
                    foreach (HtmlElement he in hl)
                    {
                        string src = he.GetAttribute("data-src");
                        bool noimage = he.GetAttribute("src") == UsedMachinerySite.NO_IMAGE_URL;
                        if (src.StartsWith(UsedMachinerySite.IMAGE_URL) && !noimage)
                        {
                            src = UsedMachinerySite.PAGE_HOME + src;
                            //下载
                            string imagePath = CurrentCompany.GetFilePath(GetLastStringByUrl(src, true));
                            //下载存盘
                            SpiderWebClient.DownloadFile(src, imagePath);
                            CurrentProduct.Images.Add(imagePath);

                        }
                    }
                    CurrentProduct.IsDonen = true;
                    if (pIndex != -1 && pIndex != CurrentCompany.Products.Count - 1)
                    {
                        //继续任务
                        Product product = CurrentCompany.Products[++pIndex];
                        CurrentProduct = product;

                        _webBrowser.Navigate(UsedMachinerySite.PRODUCT_DETAIL_URL + product.Id);
                    }
                    else
                    {
                        //全部完成退出
                        GetProductDetaildDoen = true;
                    }
                }

            }
            catch (Exception ex)
            {
                SaveListFile();
                MessageBox.Show("1037 ERROR!\r\n" + ex.Message);
                //   throw ex;
            }

        }

        private void _webBrowser_Product_List_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            try
            {
                //读取产品列表ID
                HtmlDocument document = _webBrowser.Document;
                ClearMemory();
                if (document != null)
                {
                    string url = document.Url.ToString();
                    if (url.StartsWith(UsedMachinerySite.PRODUCT_LIST_URL) && url.IndexOf(CurrentCompany.Id) != -1)
                    {//URL会发生变化，所以采取模糊匹配

                    }
                    else
                    {
                        return;
                    }

                    HtmlElementCollection hl = document.GetElementsByTagName("A");
                    HtmlElement nextpage = null;
                    foreach (HtmlElement he in hl)
                    {
                        string href = he.GetAttribute("HREF");
                        if (href.StartsWith(UsedMachinerySite.PRODUCT_DETAIL_URL))
                        {
                            //提取公司ID
                            Product product = new Product();
                            product.Id = GetLastStringByUrl(href);
                            product.Name = he.InnerText;
                            CurrentCompany.Products.Add(product);
                        }
                        //查找下一页按钮
                        if (he.InnerText == UsedMachinerySite.NEXT_PAGE_TEXT)
                        {
                            nextpage = he;
                        }
                    }
                    //处理完了,切换页面
                    if (nextpage != null)
                    {
                        //#if !DEBUG
                        nextpage.InvokeMember("click");
                        return;
                        //#endif
                    }
                    //全部处理完成，改变程序状态
                    GetProductListDoen = true;
                }
            }
            catch (Exception ex)
            {
                SaveListFile();
                MessageBox.Show("1039 ERROR!\r\n" + ex.Message);
                // throw ex;
            }
        }

        private void _webBrowser_Company_Detail_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            try
            {
                HtmlDocument document = _webBrowser.Document;
                ClearMemory();
                if (document != null)
                {
                    //存在多次触发问题，使用url进行验证
                    if (GetLastStringByUrl(document.Url.ToString()) != CurrentCompany.Id)
                    {
                        return;
                    }
                    HtmlElementCollection hc = document.GetElementsByTagName("TD");
                    //0 所在地
                    CurrentCompany.Location = GetTextA(hc[0]);
                    //1連絡先住所2
                    CurrentCompany.ContactAddr = GetTextA(hc[1]);
                    //2 代表取締役
                    CurrentCompany.President = GetTextA(hc[2]);
                    //3 担当者名
                    CurrentCompany.Person = GetTextA(hc[3]);
                    //4 電話番号1
                    CurrentCompany.Phone = GetTextA(hc[4]);
                    //5 電話番号2
                    CurrentCompany.Phone2 = GetTextA(hc[5]);
                    //6 携帯
                    CurrentCompany.CellularPhone = GetTextA(hc[6]);
                    //7 FAX
                    CurrentCompany.FAX = GetTextA(hc[7]);
                    //8 倉庫住所1
                    CurrentCompany.HousewareAddr1 = GetTextA(hc[8]);
                    //9倉庫住所2
                    CurrentCompany.HousewareAddr2 = GetTextA(hc[9]);
                    //10 倉庫住所3
                    CurrentCompany.HousewareAddr3 = GetTextA(hc[10]);
                    //11 営業時間
                    CurrentCompany.BusinessHours = GetTextA(hc[11]);
                    //12 定休日
                    CurrentCompany.DayOff = GetTextA(hc[12]);
                    //13 会社紹介
                    CurrentCompany.CompanyProfile = GetTextA(hc[13]);
                    //15 取扱機種
                    CurrentCompany.HandlingUnitType = GetTextA(hc[15]);
                    //16 ホームページ（logo和homeurl）
                    string homeUrl = string.Empty;
                    HtmlElement imageHe = null;



                    if (hc[16].FirstChild.TagName == "A")
                    {
                        //没有设置HOMEPAGE的DOM元素结构
                        homeUrl = hc[16].FirstChild.FirstChild.FirstChild.Children[1].FirstChild.InnerText;
                        imageHe = hc[16].FirstChild.FirstChild.FirstChild.FirstChild.FirstChild.FirstChild;
                    }
                    else
                    {
                        //设置了HOMEPAGE的DOM元素目录
                        homeUrl = hc[16].FirstChild.FirstChild.Children[1].FirstChild.InnerText;
                        imageHe = hc[16].FirstChild.FirstChild.FirstChild.FirstChild.FirstChild;
                    }
                    string src = imageHe.GetAttribute("src");
                    if (src == UsedMachinerySite.COMPANY_DEFAULT_IMAGE_URL)
                    {
                        //没有Logo
                        CurrentCompany.Logo = string.Empty;
                    }
                    else
                    {
                        string imagePath = CurrentCompany.GetFilePath(GetLastStringByUrl(src, true));
                        //下载存盘
                        SpiderWebClient.DownloadFile(src, imagePath);
                        CurrentCompany.Logo = imagePath;
                    }

                    CurrentCompany.HomePage = string.IsNullOrEmpty(homeUrl) ? string.Empty : homeUrl;
                    //16 古物商許可番号
                    CurrentCompany.SecondHandDearlerLicenseNo = GetTextA(hc[17]);
                    //17 薦会社
                    CurrentCompany.Recommender = GetTextA(hc[18]);
                    //18 登録年月日
                    CurrentCompany.RegisterDate = GetTextA(hc[19]);
                    GetCompanyDetaildDoen = true;
                }
            }
            catch (Exception ex)
            {
                SaveListFile();
                MessageBox.Show("1058 ERROR!\r\n" + ex.Message);
                //   throw ex;
            }

        }
        private string GetTextA(HtmlElement he)
        {

            return string.IsNullOrEmpty(he.FirstChild.InnerText) ? string.Empty : he.FirstChild.InnerText;
        }
        private string GetTextB(HtmlElement he)
        {

            return string.IsNullOrEmpty(he.InnerText) ? string.Empty : he.InnerText;
        }
        private string GetLastStringByUrl(string url)
        {
            return GetLastStringByUrl(url, false);

        }
        private string GetLastStringByUrl(string url, bool b)
        {

            string str = url.Substring(url.LastIndexOf('/') + 1);
            if (b)
            {
                str = str.Remove(str.LastIndexOf('?'));
            }
            return str;
        }

        private void _webBrowser_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            lblUrl.Text = Convert.ToString(_webBrowser.Url);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == folderBrowserDialog1.ShowDialog())
            {
                txtDataPath.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            isRun = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            isRun = false;
            button4.Enabled = false;
        }
    }
    public class Company
    {
        public static int COMPANY_SUM;
        public bool IsDonen { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Folder { get; set; }
        public List<Product> Products { get; set; }
        public string Location { get; set; }
        public string ContactAddr { get; set; }
        public string President { get; set; }
        public string Person { get; set; }
        public string Phone { get; set; }
        public string Phone2 { get; set; }
        public string CellularPhone { get; set; }
        public string FAX { get; set; }
        public string HousewareAddr1 { get; set; }
        public string HousewareAddr2 { get; set; }
        public string HousewareAddr3 { get; set; }
        public string BusinessHours { get; set; }
        public string Logo { get; set; }
        public string HandlingUnitType { get; set; }
        public string CompanyProfile { get; set; }
        public string DayOff { get; set; }
        public string HomePage { get; internal set; }
        public string RegisterDate { get; internal set; }
        public string Recommender { get; internal set; }
        public string SecondHandDearlerLicenseNo { get; internal set; }

        public string GetFilesFolder()
        {
            string filesFolder = Folder + "\\files";
            if (!Directory.Exists(filesFolder))
            {
                Directory.CreateDirectory(filesFolder);
            }
            return filesFolder;
        }
        public string GetFilePath(string fileName)
        {
            return GetFilesFolder() + "\\" + fileName;
        }
        /// <summary>
        /// 把公司的产品输出到csv
        /// </summary>
        public void PrintProducts()
        {

            string str = "\"CompanId\",";
            str += "\"会社名\",";
            str += "\"機械ID\",";
            str += "\"機械名\",";
            str += "\"画像\",";
            str += "\"在庫コード\",";
            str += "\"メーカー\",";
            str += "\"型式\",";
            str += "\"年式\",";
            str += "\"価格\",";
            str += "\"主仕様\",";
            str += "\"付属品\",";
            str += "\"現状\",";
            str += "\"機械寸法\",";
            str += "\"機械重量\",";
            str += "\"見積条件\",";
            str += "\"機械リンクURL\",";
            str += "\"在庫場所\",";
            str += "\"在庫場所詳細\",";
            str += "\"カテゴリー\",";
            str += "\"アクセス数\"\r\n";
            foreach (Product product in Products)
            {
                str += "\"" + Id + "\"," + "\"" + Name + "\"," + product.ToString() + "\r\n";
            }
            string path = Folder + "\\Products.csv";
            File.WriteAllText(path, str, Common.JP_ENCODING);
        }
        /// <summary>
        /// 把公司信息输出到CSV
        /// </summary>
        public void Print()
        {
            string str = "\"CompanyId\",";
            str += "\"会社名\",";
            str += "\"所在地\",";
            str += "\"連絡先住所２\",";
            str += "\"代表取締役\",";
            str += "\"担当者\",";
            str += "\"電話番号\",";
            str += "\"電話番号２\",";
            str += "\"携帯\",";
            str += "\"FAX\",";
            str += "\"倉庫住所１\",";
            str += "\"倉庫住所２\",";
            str += "\"倉庫住所３\",";
            str += "\"営業時間\",";
            str += "\"定休日\",";
            str += "\"会社紹介\",";
            str += "\"取扱機種\",";
            str += "\"Logo\",";
            str += "\"ホームページ\",";
            str += "\"古物商許可番号\",";
            str += "\"推薦会社\",";
            str += "\"登録年月日\"";
            str += "\r\n";
            str += ToString();
            string path = Folder + "\\Company.csv";
            File.WriteAllText(path, str, Common.JP_ENCODING);
        }
        /// <summary>
        /// 输出CSV格式
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            COMPANY_SUM++;
            string str = "\"" + Id + "\",";
            str += "\"" + Name + "\",";
            str += "\"" + Location + "\",";
            str += "\"" + ContactAddr + "\",";
            str += "\"" + President + "\",";
            str += "\"" + Person + "\",";
            str += "\"" + Phone + "\",";
            str += "\"" + Phone2 + "\",";
            str += "\"" + CellularPhone + "\",";
            str += "\"" + FAX + "\",";
            str += "\"" + HousewareAddr1 + "\",";
            str += "\"" + HousewareAddr2 + "\",";
            str += "\"" + HousewareAddr3 + "\",";
            str += "\"" + BusinessHours + "\",";
            str += "\"" + DayOff + "\",";
            str += "\"" + CompanyProfile + "\",";
            str += "\"" + HandlingUnitType + "\",";
            str += "\"" + Logo + "\",";
            str += "\"" + HomePage + "\",";
            str += "\"" + SecondHandDearlerLicenseNo + "\",";
            str += "\"" + Recommender + "\",";
            str += "\"" + RegisterDate + "\"";
            return str;
        }
        public Company()
        {
            Products = new List<Product>();
        }


    }

    public class Product
    {
        public static int PRODUCT_SUM;

        public bool IsDonen { get; set; }
        /// <summary>
        /// 产品编号
        /// </summary>
        public string Id { get; set; }
        public string Name { get; set; }
        /// <summary>
        /// 分类信息
        /// </summary>
        public string Category { get; set; }
        /// <summary>
        /// 产品图片 最多四张
        /// </summary>
        public List<string> Images { get; set; }

        public string StockCode { get; set; }

        public string Manufacturter { get; set; }
        public string Model { get; set; }
        public string ModelYear { get; set; }
        public string AskingPrice { get; set; }
        public string MainSpecification { get; set; }
        public string Accessory { get; set; }
        public string Condition { get; set; }
        public string Size { get; set; }
        public string Weight { get; set; }
        public string QuotationConditions { get; set; }
        public string InformationURL { get; set; }
        public string StockLocation { get; set; }
        public string StockLocationDetail { get; set; }

        public int AccessCount { get; set; }
        public Product()
        {
            Images = new List<string>();
        }
        /// <summary>
        /// 输出CSV格式
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            PRODUCT_SUM++;
            string str = "\"" + Id + "\",";
            str += "\"" + Name + "\",";
            str += "\"" + string.Join(",", Images.ToArray()) + "\",";
            str += "\"" + StockCode + "\",";
            str += "\"" + Manufacturter + "\",";
            str += "\"" + Model + "\",";
            str += "\"" + ModelYear + "\",";
            str += "\"" + AskingPrice + "\",";
            str += "\"" + MainSpecification + "\",";
            str += "\"" + Accessory + "\",";
            str += "\"" + Condition + "\",";
            str += "\"" + Size + "\",";
            str += "\"" + Weight + "\",";
            str += "\"" + QuotationConditions + "\",";
            str += "\"" + InformationURL + "\",";
            str += "\"" + StockLocation + "\",";
            str += "\"" + StockLocationDetail + "\",";
            str += "\"" + Category + "\",";
            str += "\"" + AccessCount + "\"";
            return str;
        }

    }
    public class UsedMachinerySite
    {
        public const string NO_IMAGE_URL = "/assets/images/noimage100x75.png";
        public const string PAGE_HOME = "https://www.jp.usedmachinery.bz";
        public const string IMAGE_URL = "/assets/images/jpmachines";

        public const string COMPANY_DEFAULT_IMAGE_URL = "https://www.jp.usedmachinery.bz/assets/images/noimage60x45.png";
        /// <summary>
        /// 下一页字符
        /// </summary>
        public const string NEXT_PAGE_TEXT = "次へ>";
        /// <summary>
        /// 公司一览
        /// </summary>
        public const string COMPANY_LIST_URL = "https://www.jp.usedmachinery.bz/members/list";
        /// <summary>
        /// 产品一览,需要+公司ID
        /// </summary>
        public const string PRODUCT_LIST_URL = "https://www.jp.usedmachinery.bz/members/general_list_id/";

        /// <summary>
        /// 公司详细，需要+公司ID
        /// </summary>
        public const string COMPANY_DETAIL_URL = "https://www.jp.usedmachinery.bz/members/general_view/";
        /// <summary>
        /// 产品详细，需要+产品ID
        /// </summary>
        public const string PRODUCT_DETAIL_URL = "https://www.jp.usedmachinery.bz/machines/general_view/";
    }
    public class Common
    {
        public static readonly Encoding JP_ENCODING = Encoding.GetEncoding("SHIFT_JIS");
        public static void WrtieCompanList(string path, List<Company> companyList)
        {
            using (StreamWriter sw = new StreamWriter(path, false))
            {

                sw.Write(JsonConvert.SerializeObject(companyList));
            }
        }
        /// <summary>
        /// 从文件中读取消息历史，并加载到内存中
        /// </summary>
        public static List<Company> ReadCompanList(string path)
        {
            if (!File.Exists(path))
            {
                return new List<Company>();
            }

            string jsonText = File.ReadAllText(path);
            List<Company> companyList = JsonConvert.DeserializeObject<List<Company>>(jsonText);
            return companyList;

        }


    }
    public class SpiderWebClient
    {
        private static Queue<string> JobQueue = new Queue<string>();


        public bool IsEmptyJobQueue()
        {
            return JobQueue.Count == 0;
        }
        public string DownloadFile(string url, string path)
        {
            if (File.Exists(path))
            {
                return path;
            }
            try
            {
                WebClient _WebClient = new WebClient();
                _WebClient.Headers.Add("User-Agent: Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1; Trident/4.0; .NET4.0E; .NET4.0C; InfoPath.2; .NET CLR 2.0.50727; .NET CLR 3.0.04506.648; .NET CLR 3.5.21022; .NET CLR 3.0.4506.2152; .NET CLR 3.5.30729; SE 2.X MetaSr 1.0)");
                _WebClient.DownloadFileCompleted += Wc_DownloadFileCompleted;
                JobQueue.Enqueue(url);
                System.Diagnostics.Debug.WriteLine(url);
                _WebClient.DownloadFileAsync(new Uri(url), path);
                return path;
            }
            catch (Exception ex)
            {

                return string.Empty;
            }
        }

        private void Wc_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            JobQueue.Dequeue();
        }
    }
}
