using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ESekisanSpider
{
    public partial class Form1 : Form
    {
        const int BUFFER_SIZE = 1024;

        [DllImport("KERNEL32.DLL", EntryPoint = "SetProcessWorkingSetSize", SetLastError = true, CallingConvention = CallingConvention.StdCall)]
        internal static extern bool SetProcessWorkingSetSize(IntPtr pProcess, int dwMinimumWorkingSetSize, int dwMaximumWorkingSetSize);

        [DllImport("KERNEL32.DLL", EntryPoint = "GetCurrentProcess", SetLastError = true, CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr GetCurrentProcess();
        private bool _Expanding = true;
        private int _PrintLoadTime = 0;
        private HtmlElement _CurrentLabel = null;
        //下载队列
        private List<Task> _Task = new List<Task>();
        CancellationTokenSource _Cts = new CancellationTokenSource();
        //用来作为计数器
        private static ConcurrentQueue<int> _DownLoadQueue = new ConcurrentQueue<int>();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SetAppState("自動ログイン中....");
            LoginIn();

#if DEBUG
            txtSavePath.Text = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
#endif
            //_webBrowser.Navigate(@"C:\Users\luhu\Desktop\積算資料電子版.htm");
        }

        private void LoginIn()
        {
            btnLoginAndLogOut.Enabled = false;
            ClearWebBrowserEvent();
            _webBrowser.DocumentCompleted += _webBrowser_LoginPage_DocumentCompleted;
            _webBrowser.Navigate(EsEkisanSite.SITE_HOME);
            btnLoginAndLogOut.Text = "ログアウト";
        }

        private void LogOut()
        {
            btnLoginAndLogOut.Enabled = false;
            ClearWebBrowserEvent();
            _webBrowser.DocumentCompleted += _webBrowser_LogOut_DocumentCompleted;
            _webBrowser.Navigate(EsEkisanSite.SITE_MENU_PAGE2);
            btnLoginAndLogOut.Text = "ログイン";
        }

        private void ClearWebBrowserEvent()
        {
            //清除登录事件
            _webBrowser.DocumentCompleted -= _webBrowser_LoginPage_DocumentCompleted;
            //清除登出事件
            _webBrowser.DocumentCompleted -= _webBrowser_LogOut_DocumentCompleted;
        }

        private void _webBrowser_LogOut_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            HtmlDocument document = _webBrowser.Document;
            string url = GetCurrentUrl();
            if (url == EsEkisanSite.SITE_MENU_PAGE2)
            {
                _webBrowser.Navigate(EsEkisanSite.SITE_HOME);
            }
            else if (url == EsEkisanSite.SITE_HOME)
            {
                _webBrowser.DocumentCompleted -= _webBrowser_LogOut_DocumentCompleted;
                //  MessageBox.Show("退出网站成功");
                btnLoginAndLogOut.Enabled = true;
            }
        }

        /// <summary>
        /// 登录模块
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _webBrowser_LoginPage_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

            HtmlDocument document = _webBrowser.Document;
            string url = GetCurrentUrl();
            if (url.StartsWith(EsEkisanSite.SITE_HOME))
            {
                HtmlElement idt7 = document.GetElementById("MNU001R01:j_idt7");
                if (idt7.Children.Count > 0)
                {
                    _webBrowser.DocumentCompleted -= _webBrowser_LoginPage_DocumentCompleted;
                    MessageBox.Show("エラーメッセジーを発見、メッセジーをチェックしてください。プログラムを中止します。");
                    btnLoginAndLogOut.Enabled = true;
                    //禁用按钮
                    return;
                }
                HtmlElement contractId = document.GetElementById("MNU001R01:contractId");
                HtmlElement userId = document.GetElementById("MNU001R01:userId");
                HtmlElement currentPassword = document.GetElementById("MNU001R01:currentPassword");
                HtmlElement loginButton = document.GetElementById("MNU001R01:loginBtn");

                contractId.SetAttribute("value", EsEkisanSite.CONTRACT_ID);
                userId.SetAttribute("value", EsEkisanSite.USER_ID);
                currentPassword.SetAttribute("value", EsEkisanSite.CURRENT_PASSWORD);

                loginButton.InvokeMember("click");

            }
            else if (url == EsEkisanSite.SITE_USER_PAGE)
            {
                //进入到数据一览页面
                HtmlElement gotoButton = document.GetElementById("USR001R01:SCR001R01Link");
                gotoButton.InvokeMember("click");
                SetAppState("プログラム準備完了....");
                SetAppState("必要な変数を設定してから「ダウンロード」をクリックしてください....");
            }
            else if (url == EsEkisanSite.SITE_DATA_LIST_PAGE)
            {
                //登录成功,清除登录事件
                btnLoginAndLogOut.Enabled = true;
                _webBrowser.DocumentCompleted -= _webBrowser_LoginPage_DocumentCompleted;
            }
        }
        private HtmlElement GetHtmlElementByName(string name)
        {
            HtmlDocument document = _webBrowser.Document;
            HtmlElementCollection hc = document.GetElementsByTagName("input");
            foreach (HtmlElement he in hc)
            {
                string heName = he.GetAttribute("name");
                if (heName == name)
                {
                    return he;

                }
            }
            return null;


        }
        private void DownLoadFile(string filePath, string postDataStr,string cookie)
        {

            try
            {
                Encoding encoding = Encoding.UTF8;

                //触发打印表单

                string requestUrl = EsEkisanSite.SITE_DATA_LIST_PAGE;
                HttpWebRequest request = HttpWebRequest.Create(requestUrl) as HttpWebRequest;
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.Referer = requestUrl;
                request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8";
                request.UserAgent = "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_12_5) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/59.0.3071.115 Safari/537.36";
                request.KeepAlive = true;
                request.Host = "www.e-sekisan.jp";
                request.Referer = "https://www.e-sekisan.jp/ER/search/SCR001R01.jsf";

                request.Headers.Add("Accept-Encoding", "gzip, deflate, br");
                request.Headers.Add("Origin", "https://www.e-sekisan.jp");
                request.Headers.Add("Upgrade-Insecure-Requests", "1");

              
                request.Headers[HttpRequestHeader.Cookie] = cookie;

                request.Headers[HttpRequestHeader.AcceptLanguage] = "zh-CN,zh;q=0.8";
                request.Headers[HttpRequestHeader.CacheControl] = "max-age=0";



                byte[] postData = encoding.GetBytes(postDataStr);
                request.ContentLength = postData.Length;

                //任务标记入队
                _DownLoadQueue.Enqueue(1);



                Stream requestStream = request.GetRequestStream();
                requestStream.Write(postData, 0, postData.Length);
                requestStream.Close();


                RequestState myRequestState = new RequestState();
                myRequestState.form = this;
                myRequestState.request = request;
                myRequestState.filePath = filePath;
                request.BeginGetResponse(new AsyncCallback(OnResponse), myRequestState);




            }
            catch (Exception ex)
            {
                SetAppState("下载出错,错误文件" + filePath + "....");
                DeleteDownloadJob();
                //退出
                // LogOut();
                //   throw;
            }
        }

        private string GetPostDataString(string selectTodofuken)
        {
            string[] formDataKey = {
//"SCR001R01",
"SCR001R01:viewPattarn",
"SCR001R01:layoutFlag",
"SCR001R01:searchText",
"SCR001R01:selectKaisoLevel",
"SCR001R01:selectPagingTodofuken",
"SCR001R01:browseCheckFlag",
"SCR001R01:browseHendoFlag",
"SCR001R01:browseAddFlag",
"SCR001R01:browseActionKbn",
"SCR001R01:keisaishiFlag",
"SCR001R01:GDS008_keisaishiFlag",
"SCR001R01:GDS009_keisaishiFlag",
"SCR001R01:j_idt464",
"SCR001R01:j_idt468",
"SCR001R01:printGuidanceFlag",
"SCR001R01:printTitleText",
"SCR001R01:printSubtitleText",
"SCR001R01:printDateText",
"SCR001R01:printChargeText",
"SCR001R01:printTitleFlag",
"SCR001R01:printMagazinePageFlag",
"SCR001R01:printKanrenPageFlag",
"SCR001R01:printComparePriceFlag",

"SCR001R01:GDS012_printMain",
"SCR001R01:printGuidanceFlag13",
"SCR001R01:printTitleText13",
"SCR001R01:printSubtitleText13",
"SCR001R01:printDateText13",
"SCR001R01:printChargeText13",
"SCR001R01:printTitleFlag13",
"SCR001R01:printMagazinePageFlag13",
"SCR001R01:printKanrenPageFlag13",
"SCR001R01:downloadDataSelectFlag",
"SCR001R01:searchGosuYear",
"SCR001R01:searchGosuMonth",
"SCR001R01:compareGosuYear",
"SCR001R01:compareGosuMonth",
"SCR001R01:browseKeisaishiFlag",
"SCR001R01:browseSekisanPageFrom",
"SCR001R01:browseSekisanPageTo",
"SCR001R01:browseBetsuPageFrom",
"SCR001R01:browseBetsuPageTo",
"SCR001R01:browseTargetScope",
"SCR001R01:browseKikakuCode",
"SCR001R01:selectTreePosition",
"SCR001R01:guidanceViewFlag",
"SCR001R01:changeDetailPosition",
"SCR001R01:responseTime",
"SCR001R01:windowOpenCounter",
"javax.faces.ViewState" };
            //设置表单元素
            string postDataStr = string.Empty;
            postDataStr = "SCR001R01=SCR001R01&SCR001R01:selectTodofuken=" + selectTodofuken + "&";
            foreach (string key in formDataKey)
            {


                HtmlElement he = _webBrowser.Document.GetElementById(key);
                string value = string.Empty;

                if (he == null)
                {
                    //特殊处理
                    he = GetHtmlElementByName(key);
                }

                if (he != null)
                {

                    value = he.GetAttribute("value");

                    //if (string.IsNullOrEmpty(value))
                    //{

                    //}
                    //else
                    //{
                    postDataStr += key + "=" + System.Web.HttpUtility.UrlEncode(value) + "&";
                    //  }
                    if (string.IsNullOrEmpty(value))
                    {
                        System.Diagnostics.Debug.WriteLine(key + "'s value is  empty");
                    }
                }
                else
                {
                    //依旧有NULL元素
                    System.Diagnostics.Debug.WriteLine(key + " is null");
                }
            }
            if (postDataStr.EndsWith("&"))
            {
                postDataStr = postDataStr.Remove(postDataStr.Length - 1);
            }

            return postDataStr;
        }

        void OnResponse(IAsyncResult ar)
        {

            RequestState myRequestState = null;
            try
            {


                myRequestState = (RequestState)ar.AsyncState;
                HttpWebRequest request = myRequestState.request;

                myRequestState.response = (HttpWebResponse)request.EndGetResponse(ar);

                // Read the response into a Stream object.
                Stream responseStream = myRequestState.response.GetResponseStream();
                if (myRequestState.response.ContentEncoding.ToLower().Contains("gzip"))
                {
                    responseStream = new GZipStream(responseStream, CompressionMode.Decompress);
                }
                myRequestState.streamResponse = responseStream;

                myRequestState.localFileStream = new FileStream(myRequestState.filePath, FileMode.Create);
                // Begin the Reading of the contents of the HTML page and print it to the console.
                IAsyncResult asynchronousInputRead = responseStream.BeginRead(myRequestState.BufferRead, 0, BUFFER_SIZE, new AsyncCallback(ReadCallBack), myRequestState);

            }
            catch (Exception ex)
            {
                SetAppState("ダウンロードエラー" + myRequestState.filePath + "....");
                DeleteDownloadJob();


            }

            //  HttpWebResponse response = (HttpWebResponse)request.EndGetResponse(ar);
            //  System.IO.Stream stream = response.GetResponseStream();
        }
        private static void ReadCallBack(IAsyncResult asyncResult)
        {

            RequestState myRequestState = null;
            try
            {

                myRequestState = (RequestState)asyncResult.AsyncState;
                Stream responseStream = myRequestState.streamResponse;
                int read = responseStream.EndRead(asyncResult);
                // Read the HTML page and then print it to the console.
                if (read > 0)
                {
                    myRequestState.localFileStream.Write(myRequestState.BufferRead, 0, read);

                    IAsyncResult asynchronousResult = responseStream.BeginRead(myRequestState.BufferRead, 0, BUFFER_SIZE, new AsyncCallback(ReadCallBack), myRequestState);
                    return;
                }
                else
                {
                    DeleteDownloadJob();
                    myRequestState.localFileStream.Close();
                    responseStream.Close();
                }

            }
            catch (WebException e)
            {
                myRequestState.form.SetAppState("ダウンロードエラー" + myRequestState.filePath + "....");
                DeleteDownloadJob();
            }


        }

        private static void DeleteDownloadJob()
        {
            int result;
            while (!_DownLoadQueue.TryDequeue(out result))
            {

                System.Threading.Thread.Sleep(100);
            }
        }

        private string GetCurrentUrl()
        {
            try
            {
                return _webBrowser.Url.ToString();
            }
            catch (Exception ex)
            {

                return string.Empty;
            }
        }

        private void _webBrowser_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            //输出URL,释放内存
            lblCurrentUrl.Text = "当前URL:" + GetCurrentUrl();
            IntPtr pHandle = GetCurrentProcess();
            SetProcessWorkingSetSize(pHandle, -1, -1);

            //屏蔽alert confirm
            //IHTMLWindow2 win = (IHTMLWindow2)_webBrowser.Document.Window.DomWindow;
            //string s = @"function confirm() {";
            //s += @"return true;";
            //s += @"}";
            //s += @"function alert() {}";
            //win.execScript(s, "javascript");
        }


        private void btnLoginAndLogOut_Click_1(object sender, EventArgs e)
        {
            if (btnLoginAndLogOut.Text == "ログイン")
            {
                LoginIn();
            }
            else
            {
                LogOut();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtSavePath.Text))
                {
                    MessageBox.Show("ァイルを保存する場所を選んでください。");
                    return;
                }


                string path = txtSavePath.Text + "\\単価リスト(一覧)";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                button1.Enabled = false;
                // btnLoginAndLogOut.Enabled = false;
                SetAppState("==============================");
                SetAppState("ダウンロード作業を始めています、プログラムを中止しないでください....");
                //下载所有可下载节点
                ProcessNode();
                //等待所有下载任务完成
                while (!_DownLoadQueue.IsEmpty)
                {
                    System.Threading.Thread.Sleep(200);
                    Application.DoEvents();
                }
                button1.Enabled = true;
                SetAppState("任務完了!");
            }
            catch (Exception ex)
            {
                //   LogOut();
                //throw;
            }
        }

        private void ProcessNode()
        {
            List<int> nodeIdList = new List<int>();
            _PrintLoadTime = 0;
            _DownLoadQueue = null;
            _DownLoadQueue = new ConcurrentQueue<int>();

            _webBrowser.DocumentCompleted += _webBrowser_DocumentCompleted;
            //  PTag:
            HtmlElement div = _webBrowser.Document.GetElementById("kikakuList");
            HtmlElementCollection hc = div.GetElementsByTagName("label");
            for (int i = 0; i < hc.Count; i++)
            {

                HtmlElement he = hc[i];

                if (string.IsNullOrEmpty(he.InnerText))
                {
                    continue;
                }
                if (he.OuterHtml.IndexOf("kikakuSelect") == -1)
                {
                    continue;
                }
                if (he.GetAttribute("onclick") != EsEkisanSite.TREE_NO_CLICK_TAG)
                {


                    Match mt = Regex.Match(he.OuterHtml, @"\d+");
                    int id = Convert.ToInt32(mt.Value);
                    if (nodeIdList.Contains(id))
                    {
                        continue;
                    }
                    else
                    {
                        nodeIdList.Add(id);

                    }
                    _CurrentLabel = he;
                    //清除已有的table元素
                    HtmlElement table = GetTableObject();
                    table.OuterHtml = "";
                    he.InvokeMember("click");
                    //while (true)
                    //{
                    //    if (_webBrowser.Document.Body.OuterHtml != content)
                    //    {
                    //        //内容不等 加载完毕   
                    //        break;
                    //    }

                    //    System.Threading.Thread.Sleep(100);
                    //}
                    //右侧数据加载完毕
                    table = GetTableObject();
                    HtmlElement select = _webBrowser.Document.GetElementById("SCR001R01:selectPagingTodofuken");
                    List<string> regions = new List<string>();
                    if (select != null)
                    {
                        HtmlElementCollection options = select.GetElementsByTagName("option");

                        for (int j = 0; j < options.Count; j++)
                        {
                            regions.Add(options[j].GetAttribute("value"));
                        }
                        _webBrowser_DATA_DocumentCompleted(id, regions);
                        //for (int k = 0; k < EsEkisanSite.REGIN_NUMBERS.Count; k++)
                        //{
                        //    string key = EsEkisanSite.REGIN_NUMBERS[k];
                        //    HtmlElement option = GetOption(select, key);

                        //    if (k <= _CURRENT_REGION)
                        //    {
                        //        continue;
                        //    }

                        //    if (option == null)
                        //    {
                        //        //没有这个县的数据
                        //    }
                        //    else
                        //    {
                        //        _CURRENT_REGION = k;
                        //        //清除已有的数据
                        //        table = GetTableObject();
                        //        table.OuterHtml = "";



                        //        //下载操作
                        //        _webBrowser_DATA_DocumentCompleted(id, key);

                        //        //下载导致页面切换，当前节点还要二次处理
                        //        i--;
                        //        break;

                        //    }
                        //}


                    }
                    else
                    {
                        //  table = GetTableObject();
                        //  table.OuterHtml = "";
                        regions.Add("00");
                        _webBrowser_DATA_DocumentCompleted(id, regions);

                    }

                    //会引起重复加载 导致集合空掉
                    hc = _webBrowser.Document.GetElementsByTagName("label");
                    //      hc = _webBrowser.Document.GetElementsByTagName("label");
                    //  goto PTag;
                    // break;


                }
            }
            _webBrowser.DocumentCompleted -= _webBrowser_DocumentCompleted;



        }

        private void _webBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            _PrintLoadTime++;
        }

        private void _webBrowser_DATA_DocumentCompleted(int id, List<string> reginos)
        {
            //计算保存位置
            string text = _CurrentLabel.InnerText;
            //   HtmlElement tr = _CurrentLabel.Parent.Parent;

            //HtmlElement span = _webBrowser.Document.GetElementById("SCR001R01:kakakuTreeGroup");
            //HtmlElementCollection hc = span.GetElementsByTagName("tr");

            //foreach (HtmlElement he in hc)
            //{
            //    if (he == tr)
            //    {

            //        break;
            //    }
            //    HtmlElementCollection ls = he.GetElementsByTagName("label");



            //    if (ls.Count > 0)
            //    {
            //        HtmlElement label = ls[0];
            //        if (label.GetAttribute("onclick") == EsEkisanSite.TREE_NO_CLICK_TAG)
            //        {
            //            string text = label.InnerText;

            //            if (!string.IsNullOrEmpty(text) && text != EsEkisanSite.TREE_ROOT_TEXT)
            //            {
            //                dirPath.Append("\\");
            //                dirPath.Append(text);
            //            }
            //        }
            //        else
            //        {
            //            break;//跳出
            //        }



            //    }

            //}





            //
            //while (printBtn == null)
            //{
            //    System.Threading.Thread.Sleep(100);
            //    Application.DoEvents();
            //    printBtn = GetHtmlElementByName("SCR001R01:j_idt129");
            //}
            HtmlElement table = GetTableObject();
            HtmlElement printBtn = GetHtmlElementByName("SCR001R01:j_idt129");
            while (printBtn == null || printBtn.OuterHtml.IndexOf("disabled") != -1)
            {
                printBtn = GetHtmlElementByName("SCR001R01:j_idt129");
                System.Threading.Thread.Sleep(100);
                Application.DoEvents();
                System.Diagnostics.Debug.WriteLine("等待打印按钮");
            }

            //System.Threading.Thread.Sleep(10000);
            printBtn.InvokeMember("click");
            //   _webBrowser.Document.InvokeScript("listHeaderPrint");
            _PrintLoadTime = 0;

            int sum = 0;
            while (_PrintLoadTime != 1)
            {
                sum += 100;
                if (sum % EsEkisanSite.MAX_TIME == 0)
                {
                    printBtn.InvokeMember("click");
                    //  _webBrowser.Document.InvokeScript("listHeaderPrint");
                }
                System.Diagnostics.Debug.WriteLine("等待激活打印窗体");
                System.Threading.Thread.Sleep(100);
                Application.DoEvents();

            }


            //多县下载
            for (int i = 0; i < reginos.Count; i++)
            {
                string regin_number = reginos[i];


                StringBuilder dirPath = new StringBuilder(txtSavePath.Text + "\\単価リスト(一覧)");
                string sDirPath = dirPath.ToString();
                if (!Directory.Exists(sDirPath))
                {
                    Directory.CreateDirectory(sDirPath);
                }
                dirPath.Append("\\");
                dirPath.Append(id);
                dirPath.Append("_");
                dirPath.Append(regin_number);
                dirPath.Append("_");
                dirPath.Append(text);
                dirPath.Append(EsEkisanSite.DOWNLOAD_FILE_TYPE);
                string filePath = dirPath.ToString();



                if (File.Exists(filePath) && false)
                {
                    //文件存在不下载
                    return;
                }
                else
                {
                    StringBuilder sb = new StringBuilder("処理中 <");
                    sb.Append(regin_number);
                    sb.Append(" ");
                    sb.Append(text);

                    sb.Append("> ,保存したファイルの場所は:");
                    sb.Append(filePath);
                    SetAppState(sb.ToString());


                    string http_regin_number = regin_number == "00" ? "" : regin_number;
                    string postDataStr = GetPostDataString(http_regin_number);
                    string cookie = Convert.ToString(FullWebBrowserCookie.GetCookieInternal(new Uri(GetCurrentUrl()), false));

                    Action action = () =>
                    {

                        DownLoadFile(filePath, postDataStr, cookie);
                    };

                    Task taks = new Task(action);
                    //最多5个线程下载
                    while (_Task.Count >= 5)
                    {
                        for (int k = _Task.Count - 1; k >= 0; k--)
                        {
                            if (_Task[k].IsCompleted)
                            {
                                _Task.RemoveAt(k);
                            }
                        }
                        System.Threading.Thread.Sleep(100);
                        Application.DoEvents();
                    }
                    _Task.Add(taks);
                    taks.Start();


                }
                Application.DoEvents();
            }


            //关闭打印窗体

            //while (_webBrowser.Document.GetElementById("GUIDANCE_SPAN") == null)
            //{
            //    System.Threading.Thread.Sleep(100);
            //    Application.DoEvents();
            //}

            HtmlElement span = _webBrowser.Document.GetElementById("GUIDANCE_SPAN");
            sum = 0;
            while (span == null)
            {
                sum += 100;
                System.Threading.Thread.Sleep(100);
                Application.DoEvents();
                span = _webBrowser.Document.GetElementById("GUIDANCE_SPAN");
                if (sum == EsEkisanSite.MAX_TIME)
                {
                    break;
                }
                System.Diagnostics.Debug.WriteLine("查找打印窗体，查找到之后关闭.");
            }
            if (span != null)
            {
                HtmlElementCollection hc = span.GetElementsByTagName("img");
                foreach (HtmlElement he in hc)
                {
                    string src = he.GetAttribute("src");

                    if (src.EndsWith(EsEkisanSite.CLOSE_BUTTON_ICON))
                    {
                        he.InvokeMember("click");
                    }
                }
            }

            table.OuterHtml = String.Empty;
            //sum = 0;
            //while (true)
            //{
            //    sum += 100;
            //    System.Threading.Thread.Sleep(100);
            //    Application.DoEvents();

            //    if (sum ==10000)
            //    {
            //        break;
            //    }
            //    System.Diagnostics.Debug.WriteLine("程序等待十秒");
            //}
            //  _webBrowser.Document.GetElementById("GUIDANCE_SPAN").OuterHtml = "";
            //  _webBrowser.Document.GetElementById("FILTER_DIV").OuterHtml = "";



        }


        private void button2_Click(object sender, EventArgs e)
        {
            if (_FolderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                txtSavePath.Text = _FolderBrowserDialog.SelectedPath;
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            _Cts.Cancel();
            _Expanding = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
           {
                //展开TREE
                if (MessageBox.Show("展開は約10分かかります。展開しますか。", "", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                {
                    return;
                }
               
                var ct = _Cts.Token;

                this.Cursor = Cursors.WaitCursor;
                SetAppState("TREEを展開し始めました、プログラムを閉じないでください....");
                button4.Enabled = false;
                Action action = () => { expandTree(ct); };

                action.Invoke();
              //  Task task = new Task(action);
               // task.Start();
                //while (!task.IsCanceled)
                //{
                //    Application.DoEvents();
                //    System.Threading.Thread.Sleep(100);
                //}
                SetAppState("TREEがすべて展開しました。");
                button4.Enabled = true;
                this.Cursor = Cursors.Default;
            }
            catch (AggregateException exeption)
            {
               // SetAppState("Tree展开任务已经被取消!");

            }
        }
        private void expandTree(CancellationToken ct)
        {
            List<int> nodeIdList = new List<int>();
            HtmlElement span = null;
            HtmlElementCollection hc = null;
           // this.Invoke(new EventHandler(delegate
          //  {
                  span = _webBrowser.Document.GetElementById("kikakuListDiv");
          //  }));
            hc = span.GetElementsByTagName("img");
            for (int i = 0; i < hc.Count; i++)
            {
                HtmlElement he = hc[i];

                Application.DoEvents();
                string src = he.GetAttribute("src");

                if (src.IndexOf(EsEkisanSite.TREE_ICON_PLUS) != -1 && he.Parent != null)
                {
                    HtmlElement lable = he.Parent.NextSibling.FirstChild;
                    if (lable.GetAttribute("onclick") == EsEkisanSite.TREE_NO_CLICK_TAG)
                    {
                        Match mt = Regex.Match(he.OuterHtml, @"\d+");
                        int id = Convert.ToInt32(mt.Value);

                        if (nodeIdList.Contains(id))
                        {
                            continue;
                        }
                        else
                        {
                            nodeIdList.Add(id);
                        }
                        //  he.InvokeMember("click");
                        //   this.Invoke(new EventHandler(delegate
                        {
                            _webBrowser.Document.InvokeScript("treeOpCl", new object[] { id });
                            //   }));
                            string str = "treeOpCl('" + id + "')\" class=treebtn alt=\"\" src=\"https://www.e-sekisan.jp/ER/images/minus.gif\">";
                            int sum = 0;
                            while (span.OuterHtml.IndexOf(str) == -1)
                            {

                                sum += 50;
                                if (sum % 800 == 0)
                                {
                                    //  this.Invoke(new EventHandler(delegate
                                    //  {
                                    _webBrowser.Document.InvokeScript("treeOpCl", new object[] { id });
                                    //  }));
                                }
                                Application.DoEvents();
                                System.Threading.Thread.Sleep(50);
                                //  this.Invoke(new EventHandler(delegate
                                //  {
                                span = _webBrowser.Document.GetElementById("kikakuListDiv");
                                //  }));
                                //等待这个节点展开  只能串行执行
                                System.Diagnostics.Debug.WriteLine("等待节点" + id + "完成");
                            }
                            //  System.Diagnostics.Debug.WriteLine(he.OuterHtml);
                            if (_Expanding)
                            {
                                //    ct.ThrowIfCancellationRequested();
                                hc = span.GetElementsByTagName("img");
                                //while (hc.Count == 0)
                                //{
                                //    Application.DoEvents();
                                //    System.Threading.Thread.Sleep(100);
                                //    _webBrowser.Document.GetElementsByTagName("img");
                                //}

                                //span = _webBrowser.Document.GetElementById("SCR001R01:kakakuTreeGroup");
                                // hc = span.GetElementsByTagName("img");
                            }
                            else
                            {

                                _Expanding = true;
                                SetAppState("TREEを展開する任務がキャンセルされました。");
                                break;

                            }

                        }

                    }
                }
            }
          //  goto treeOpCl;
        }
        public void SetAppState(string message)
        {
            this.Invoke(new EventHandler(delegate
            {
                lbAppState.Items.Add(message);
                lbAppState.TopIndex = lbAppState.Items.Count - (int)(lbAppState.Height / lbAppState.ItemHeight);
            }));

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (btnLoginAndLogOut.Text == "ログイン")
            {
                //ok
            }
            else
            {
                MessageBox.Show("手動でシステムをロングインしてください。", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Cancel = true;
                return;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            HtmlElement select = _webBrowser.Document.GetElementById("SCR001R01:selectPagingTodofuken");

            if (select != null)
            {
                HtmlElementCollection options = select.GetElementsByTagName("option");
                for (int i = 0; i < EsEkisanSite.REGIN_NUMBERS.Count - 45; i++)
                {
                    string key = EsEkisanSite.REGIN_NUMBERS[i];
                    HtmlElement option = GetOption(select, key);
                    if (option == null)
                    {
                        //没有这个县的数据
                    }
                    else
                    {
                        HtmlElement table = GetTableObject();
                        table.OuterHtml = "";
                        option.SetAttribute("selected", "selected");
                        select.RaiseEvent("onchange");


                    }
                }
            }
            else
            {

            }
        }

        /// <summary>
        /// 等待ajax加载完毕,每次使用完必须清除该元素
        /// 作为下载ajax请求完毕的判断条件
        /// </summary>
        /// <returns></returns>
        private HtmlElement GetTableObject()
        {
            HtmlElement span = _webBrowser.Document.GetElementById("SCR001R01:kakakuListGroup");
            HtmlElementCollection hc = span.GetElementsByTagName("table");
            HtmlElement table = hc.Count > 0 ? hc[0] : null;
            while (table == null)
            {
                System.Threading.Thread.Sleep(100);
                Application.DoEvents();

                span = _webBrowser.Document.GetElementById("SCR001R01:kakakuListGroup");
                hc = span.GetElementsByTagName("table");
                table = hc.Count > 0 ? hc[0] : null;
            }
            return table;

        }
        private HtmlElement GetOption(HtmlElement select, string key)
        {
            HtmlElementCollection options = select.GetElementsByTagName("option");
            for (int i = 0; i < options.Count; i++)
            {
                if (options[i].GetAttribute("value") == key)
                {
                    return options[i];
                }
            }
            return null;
        }
    }

}
internal sealed class NativeMethods
{
    #region enums

    public enum ErrorFlags
    {
        ERROR_INSUFFICIENT_BUFFER = 122,
        ERROR_INVALID_PARAMETER = 87,
        ERROR_NO_MORE_ITEMS = 259
    }

    public enum InternetFlags
    {
        INTERNET_COOKIE_HTTPONLY = 8192, //Requires IE 8 or higher     
        INTERNET_COOKIE_THIRD_PARTY = 131072,
        INTERNET_FLAG_RESTRICTED_ZONE = 16
    }

    #endregion

    #region DLL Imports

    [SuppressUnmanagedCodeSecurity, SecurityCritical, DllImport("wininet.dll", EntryPoint = "InternetGetCookieExW", CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
    internal static extern bool InternetGetCookieEx([In] string Url, [In] string cookieName, [Out] StringBuilder cookieData, [In, Out] ref uint pchCookieData, uint flags, IntPtr reserved);

    #endregion
}


/// <SUMMARY></SUMMARY>     
/// 取得WebBrowser的完整Cookie。     
/// 因为默认的webBrowser1.Document.Cookie取不到HttpOnly的Cookie     
///      
public class FullWebBrowserCookie
{

    [SecurityCritical]
    public static string GetCookieInternal(Uri uri, bool throwIfNoCookie)
    {
        uint pchCookieData = 0;
        string url = UriToString(uri);
        uint flag = (uint)NativeMethods.InternetFlags.INTERNET_COOKIE_HTTPONLY;

        //Gets the size of the string builder     
        if (NativeMethods.InternetGetCookieEx(url, null, null, ref pchCookieData, flag, IntPtr.Zero))
        {
            pchCookieData++;
            StringBuilder cookieData = new StringBuilder((int)pchCookieData);

            //Read the cookie     
            if (NativeMethods.InternetGetCookieEx(url, null, cookieData, ref pchCookieData, flag, IntPtr.Zero))
            {
                DemandWebPermission(uri);
                return cookieData.ToString();
            }
        }

        int lastErrorCode = Marshal.GetLastWin32Error();

        if (throwIfNoCookie || (lastErrorCode != (int)NativeMethods.ErrorFlags.ERROR_NO_MORE_ITEMS))
        {
            throw new Win32Exception(lastErrorCode);
        }

        return null;
    }

    private static void DemandWebPermission(Uri uri)
    {
        string uriString = UriToString(uri);

        if (uri.IsFile)
        {
            string localPath = uri.LocalPath;
            new FileIOPermission(FileIOPermissionAccess.Read, localPath).Demand();
        }
        else
        {
            new WebPermission(NetworkAccess.Connect, uriString).Demand();
        }
    }

    private static string UriToString(Uri uri)
    {
        if (uri == null)
        {
            throw new ArgumentNullException("uri");
        }

        UriComponents components = (uri.IsAbsoluteUri ? UriComponents.AbsoluteUri : UriComponents.SerializationInfoString);
        return new StringBuilder(uri.GetComponents(components, UriFormat.SafeUnescaped), 2083).ToString();
    }
}
public class EsEkisanSite
{
 
    public readonly static string CONTRACT_ID =Utility.GetAppSetting<string>(string.Empty, "contractId");
    public readonly static string USER_ID = Utility.GetAppSetting<string>(string.Empty, "userId");
    public readonly static string  CURRENT_PASSWORD = Utility.GetAppSetting<string>(string.Empty, "currentPassword");

    public const string LOGOUT_BUTTON_EVENT_SCRIPT = "logoutEvent(true)";

    public const string SITE_HOME = "https://www.e-sekisan.jp/ER/topmenu.jsf";
    public const string SITE_USER_PAGE = "https://www.e-sekisan.jp/ER/account/USR001R01.jsf";
    public const string SITE_MENU_PAGE2 = "https://www.e-sekisan.jp/ER/account/MNU004R01.jsf";
    public const string SITE_DATA_LIST_PAGE = "https://www.e-sekisan.jp/ER/search/SCR001R01.jsf";

    public const string TREE_ICON_MINU = "/ER/images/minus.gif";
    public const string TREE_ICON_PLUS = "/ER/images/plus.gif";
    public const string TREE_NO_CLICK_TAG = "";
    public const string CLOSE_BUTTON_ICON = "/ER/images/close.png";

    public const string TREE_ROOT_TEXT = "積算資料電子版";

    public const string DOWNLOAD_FILE_TYPE = ".ZIP";
    public const int MAX_TIME = 800;

    public readonly static List<string> REGIN_NUMBERS = new List<string>() {
    "01",
    "02",
    "03",
    "04",
    "05",
    "06",
    "07",
    "08",
    "09",
    "10",
    "11",
    "12",
    "13",
    "14",
    "19",
    "20",
    "15",
    "16",
    "17",
    "18",
    "21",
    "22",
    "23",
    "24",
    "25",
    "26",
    "27",
     "28",
    "29",
    "30",
    "31",
    "32",
    "33",
    "34",
    "35",
    "36",
    "37",
    "38",
    "39",
    "40",
    "41",
    "42",
    "43",
    "44",
    "45",
    "46",
    "47",
    };

}
public class RequestState
{
    const int BUFFER_SIZE = 1024;

    public byte[] BufferRead;
    public HttpWebRequest request;
    public HttpWebResponse response;
    public Stream streamResponse;
    public Stream localFileStream;
    public string filePath;
    public ESekisanSpider.Form1 form;
    public RequestState()
    {
        filePath = string.Empty;
        BufferRead = new byte[BUFFER_SIZE];
        localFileStream = null;
        request = null;
        streamResponse = null;
    }



}
public sealed class Utility
{
    /// <summary>
    /// 获取appsetting值
    /// </summary>
    /// <typeparam name="T">要转换的类型</typeparam>
    /// <param name="defaultValue">默认值</param>
    /// <param name="key">key</param>
    /// <returns>appsetting值</returns>
    public static T GetAppSetting<T>(T defaultValue, string key)
    {
        string value = ConfigurationManager.AppSettings[key];
        if (!string.IsNullOrEmpty(value))
        {
            try
            {
                defaultValue = (T)Convert.ChangeType(value, typeof(T));
            }
            catch
            {
            }
        }
        return defaultValue;
    }
 

   
}