using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVP.Models
{
  public  class VT5Item
    {
        /**
         * 
         * VITIE5现有结构划分，
         * Agencysoft.Vt5.Views  UI层
         * Agencysoft.Vt5.Models 数据模型 VT5ITEM最重要
         * Agencysoft.Vt5.Presenters  业务数据处理层
         * 
         * Agencysoft.Vt5.Common 公用部分
         *  常量定义
         *      数据库ENUMD定义
         *  代理设置
         *  等待窗体
         *  ZIP解压
         *  启动参数生成/检查
         *
         * Agencysoft.Vt5.Library 负责VT5部分  
         *  
         *  
         * Agencysoft.Vt5.VT5B  负责和VT5通信，返回VT5ITEM实体对象
         * 
         * VTOPINTON/VTUPDATE/VTAUTH/AGNCY5 把M层和P层也要转化为DLL
         * -----------------------------------------------------------
         * 
         * MVP工作原理:
         * M-Model
         * V-View
         * P-Presenters
         * V层和P层直接挂钩，V层的事件交给P层处理。P层处理之后调用V层的函数进行表现。
         * 也就是V层负责表现，P层负责数据处理。
         * 
         * grid.show(List<Vt5Item> list);
         * grid.show(Vt5Item item);
         * grid.show(a,b,c);
         * btnCalc.changeColor(); V层接口公布btnCalc/grid
         * /
    }
}
