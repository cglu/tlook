<?php

/**
 * 此处其实也是特定需求的接口
 * @author luhu
 *
 */
class ConcreateSubject implements SplSubject
{

    private $observers, $state;

    /**
     *
     * @return the $state
     */
    public function getState()
    {
        return $this->state;
    }

    /**
     *
     * @param field_type $state            
     */
    public function setState($state)
    {
        $this->state = $state;
        $this->notify();
    }

    /*
     * (non-PHPdoc)
     * @see SplSubject::attach()
     */
    public function attach(SplObserver $observer)
    {
        // TODO Auto-generated method stub
        $this->observers[] = $observer;
    }

    /*
     * (non-PHPdoc)
     * @see SplSubject::detach()
     */
    public function detach(SplObserver $observer)
    {
        // TODO Auto-generated method stub
        if (($index = array_search($observer, $this->observers) !== false)) {
            unset($this->observers[$index]);
        }
    }

    /*
     * (non-PHPdoc)
     * @see SplSubject::notify()
     */
    public function notify()
    {
        // TODO Auto-generated method stub
        foreach ($this->observers as $observer) {
            if ($this->getState() != $observer->getState()) {
                $observer->update($this);
            }
        }
    }
}

/**
 * 为特地那个需求构建的抽象接口
 *
 * @author luhu
 *        
 */
abstract class MyObserver
{

    private $state;

    public function __construct($state)
    {
        $this->state = $state;
    }

    /**
     *
     * @return the $state
     */
    public function getState()
    {
        return $this->state;
    }

    /**
     *
     * @param field_type $state            
     */
    public function setState($state)
    {
        $this->state = $state;
    }
}

class ConcreteObserver1 extends MyObserver implements SplObserver
{

    /*
     * (non-PHPdoc)
     * @see MyObserver::__construct()
     */
    public function __construct($state)
    {
        // TODO Auto-generated method stub
        parent::__construct($state);
    }

    /*
     * (non-PHPdoc)
     * @see Observer::update()
     */
    public function update(SplSubject $sub)
    {
        // TODO Auto-generated method stub
        echo "this is concreateobserver1.state=" . $sub->getState();
        echo "\r\n;";
    }
}

class ConcreteObserver2 extends MyObserver implements SplObserver
{

    /*
     * (non-PHPdoc)
     * @see MyObserver::__construct()
     */
    public function __construct($state)
    {
        // TODO Auto-generated method stub
        parent::__construct($state);
    }

    /*
     * (non-PHPdoc)
     * @see Observer::update()
     */
    public function update(SplSubject $sub)
    {
        // TODO Auto-generated method stub
        echo "this is concreateobserver2.state=" . $sub->getState();
        echo "\r\n;";
    }
}

class ConcreteObserver3 extends MyObserver implements SplObserver
{

    /*
     * (non-PHPdoc)
     * @see MyObserver::__construct()
     */
    public function __construct($state)
    {
        // TODO Auto-generated method stub
        parent::__construct($state);
    }

    /*
     * 实际此处的$sub类型应该为ConcreateSuject；使用顶层的抽象splsubject在某些语言这样写拿不到getstate()；比如c#
     * (non-PHPdoc)
     * @see Observer::update()
     */
    public function update(SplSubject $sub)
    {
        // TODO Auto-generated method stub
        echo "this is concreateobserver3.state=" . $sub->getState();
        echo "\r\n;";
    }
}

// /////////////////////////////////////////////////////////////////////////////////////
// /////////////////////////////////////////////////////////////////////////////////////
// /////////////////////////////////////////////////////////////////////////////////////
$concreteSubject = new ConcreateSubject();
$concreateObserver1 = new ConcreteObserver1(1);
$concreateObserver2 = new ConcreteObserver2(2);
$concreateObserver3 = new ConcreteObserver3(3);
$concreteSubject->attach($concreateObserver1);
$concreteSubject->attach($concreateObserver2);
$concreteSubject->attach($concreateObserver3);
echo "subject state=1.\r\n";
// 观察者2、3执行更新操作
$concreteSubject->setState(1);
echo "subject state=2.\r\n";
// 观察者123执行鞥新
$concreteSubject->setState(2);
//这个案例继续扩展下去、可以实现多对象状态的通知和更新。通过在notify内的控制、避免发生无线通知的现象。
//抽线层次
//1.subject接口  observer接口    
//2.特定需求的 subject/observer接口
//3.具体实现suject/observer接口的类
//客户端调用


//符合开闭原则、23层次可以根据特定需求的增加而进行扩展。。