<?php

/**
 * 观察者接口
 * @author luhu
 *
 */
interface Observer
{

    public function update(Subject $sub);
}

/**
 * 对象接口
 *
 * @author luhu
 *        
 */
interface Subject
{

    public function attach(Observer $observer);

    public function dettach(Observer $observer);

    public function notify();
}
//////////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////
class ConcreteSubject implements Subject
{

    /*
     * (non-PHPdoc)
     * @see Subject::attach()
     */
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

    public function attach(Observer $observer)
    {
        // TODO Auto-generated method stub
        $this->observers[] = $observer;
    }

    /*
     * (non-PHPdoc)
     * @see Subject::dettach()
     */
    public function dettach(Observer $observer)
    {
        // TODO Auto-generated method stub
        if (($index = array_search($observer, $this->observers)) !== false) {
            unset($this->observers[$index]);
        }
    }

    /*
     * (non-PHPdoc)
     * @see Subject::notify()
     */
    public function notify()
    {
        // TODO Auto-generated method stub
        foreach ($this->observers as $observer) {
            $observer->update($this);
        }
    }
}

class ConcreateObserver1 implements Observer
{

    /*
     * (non-PHPdoc)
     * @see Observer::update()
     */
    public function update(Subject $sub)
    {
        echo "this is concreateobserver1 update function.state=" . $sub->getState()."\r\n";
        // TODO Auto-generated method stub
    }
}
class ConcreateObserver2 implements Observer
{

    /*
     * (non-PHPdoc)
     * @see Observer::update()
     */
    public function update(Subject $sub)
    {
        echo "this is concreateobserver2 update function.state=" . $sub->getState();
        // TODO Auto-generated method stub
    }
}
//////////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////
$concreateSubject=new ConcreteSubject();
$concreateObserver1=new ConcreateObserver1();
$concreateObserver2=new ConcreateObserver2();
$concreateSubject->attach($concreateObserver1);
$concreateSubject->attach($concreateObserver2);
$concreateSubject->setState(1);



