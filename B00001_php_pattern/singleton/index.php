<?php
class  Singleton{
     private static  $_instance=null;//靜態成員保存唯一實例
     /**
      * 私有構造函數，保證不能被外部訪問
      */
     private function __construct(){}
     /**
      * 靜態方法將創建這個實例的操作並保證只有一個實例被創建
      * @return Singleton
      */
     public  static  function getInstance() {
         if (!self::$_instance) {
             self::$_instance=new self();
         }
         return self::$_instance;
     }
   public function printStr(){
       echo "Singleton@printStr function";
       
   }
}
////////////////////////////////////////////////////////////////
$instance=Singleton::getInstance();
$instance->printStr();