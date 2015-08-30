<?php
// *********************************fasade视图***************************************
class SwtichFasade
{

    private $_fan = null;

    private $_aric = null;

    private $_tv = null;

    private $_light = null;

    public function __construct()
    {
        $this->_aric = new AirConditioning();
        $this->_fan = new Fan();
        $this->_tv = new TeleVistion();
        $this->_light = new Light();
    }

    /**
     * 打开所有电器
     */
    public function trunOn()
    {
        $this->_fan->trunOn();
        $this->_aric->trunOn();
        $this->_tv->trunOn();
        $this->_light->trunOn();
    }

    /**
     * 关闭电器
     */
    public function trunOff()
    {
        $this->_fan->trunOff();
        $this->_aric->trunOff();
        $this->_tv->trunOff();
        $this->_light->trunOff();
    }

    /* public static function __callstatic($method_name, $arguments)
    {
        echo "模拟调用$method_name";
        $f = new SwtichFasade();
        if (method_exists($f, $method_name)) {
            $f->$method_name();
        }
    } */
}
// *********************************组件***************************************
class Light
{

    private $is_open = 0;

    public function trunOn()
    {
        echo "打开电灯。";
        $this->is_open = 1;
    }

    public function trunOff()
    {
        echo "关闭电灯。";
        $this->is_open = 0;
    }
}

class Fan
{

    private $is_open = 0;

    public function trunOn()
    {
        echo "打开风扇。";
        $this->is_open = 1;
    }

    public function trunOff()
    {
        echo "关闭风扇。";
        $this->is_open = 0;
    }
}

class AirConditioning
{

    private $is_open = 0;

    public function trunOn()
    {
        echo "打开空调。";
        $this->is_open = 1;
    }

    public function trunOff()
    {
        echo "关闭空调。";
        $this->is_open = 0;
    }
}

class TeleVistion
{

    private $is_open = 0;

    public function trunOn()
    {
        echo "打开电视。";
        $this->is_open = 1;
    }

    public function trunOff()
    {
        echo "关闭电视。";
        $this->is_open = 0;
    }
}
// *********************************客户端调用***************************************
/*
 * 客户端调用只和fasade进行交互；从而屏蔽了组件的复杂调用。fasade提供了一个缺省的简单视图。通过重构实现了迪米佳法则
 *
 */
$sf = new SwtichFasade();
// 打开电器
$sf->trunOn();
$sf->trunOff();

 

