<?php

/**
 * 具體迭代器
 * @author root
 *
 */
class ConcreteIterator implements Iterator
{

    private $_arr;

    private $_key;

    public function __construct($arr)
    {
        $this->_arr = $arr;
        $this->_key = 0;
    }

    /*
     * (non-PHPdoc)
     * @see Iterator::current()
     */
    public function current()
    {
        return $this->_arr[$this->_key];
        // TODO Auto-generated method stub
    }

    /*
     * (non-PHPdoc)
     * @see Iterator::key()
     */
    public function key()
    {
        return $this->_key;
        // TODO Auto-generated method stub
    }

    /*
     * (non-PHPdoc)
     * @see Iterator::next()
     */
    public function next()
    {
        return ++ $this->_key;
        // TODO Auto-generated method stub
    }

    /*
     * (non-PHPdoc)
     * @see Iterator::rewind()
     */
    public function rewind()
    {
        $this->_key = 0;
        // TODO Auto-generated method stub
    }

    /*
     * (non-PHPdoc)
     * @see Iterator::valid()
     */
    public function valid()
    {
        return isset($this->_arr[$this->_key]);
        // TODO Auto-generated method stub
    }
}
/**
 * 聚合類（容器）
 * @author root
 *
 */
class  ConcreteAggregate implements IteratorAggregate{
    private $_arr;
    public function __construct(){
        $this->_arr=[];
    } 
 /**
     * @param field_type $_arr
     */
    public function setVal($value)
    {
        $this->_arr[] = $value;
    }

 /* (non-PHPdoc)
     * @see IteratorAggregate::getIterator()
     */
    public function getIterator()
    {
        return new ConcreteIterator($this->_arr);
        // TODO Auto-generated method stub
        
    }
}
////////////////////////////////////////////////////////////////////////////////////
//使用自定義的迭代器便利array
$list=array(1,2,3);
$iterator=new ConcreteIterator($list);
foreach ($iterator as $key=>$value){
    echo $key."=>".$value;
    echo "<br/>";
}
echo "<hr/>";
///////////////////////////////////////////////////////////////////////////////////
//自定義容器
$my_list=new ConcreteAggregate();
$my_list->setVal(1);
$my_list->setVal(2);
$my_list->setVal(3);
foreach ($my_list as $key=>$value){
    echo $key."=>".$value;
    echo "<br/>";  
}
echo "<hr/>";
$integer=0;
foreach ($integer as $key=>$value){
    echo $key."=>".$value;
    echo "<br/>";
}
////////////////////////////////////////////////////////////
//1.聚合類存儲數據，遍歷數據
//2.聚合類和迭代器的掛鉤通過兩種方式
// 2.1.外部實例化迭代器，使用場景：滿足不同的遍歷需求
//  2.2.聚合類實現IteratorAggregate接口(getIterator)
//3.猜測：foreach在第一步會判斷變量是否是iterratorAggregate子類，如果是則執行getIterator函數，獲取
//迭代器對象，然後進行遍歷作業;如果本身就是迭代器對象，則執行進行遍歷作業，否則出錯
