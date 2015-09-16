<?php
/**
 * 單件模式並不是說一個類只能有一個實例
 * @author root
 *
 */
class User
{

    private static $_instances = [];

    private $uid;
      
    private function __construct($uid = 0)
    {
        $this->uid = $uid;
    }

    public static function getInstance($uid)
    {
        if (!self::$_instances||!isset(self::$_instances[$uid])) {
            echo "創建新的user對象<br/>";
            self::$_instances[$uid] = new self($uid);
        }
        return self::$_instances[$uid];
    }

    public function printUser()
    {
        echo "my uid={$this->uid}";
    }
}
// ////////////////////////////////////////////////////////////////////////
//場景：一臺PC，多個QQ登錄，每個QQ只能登錄一次
$u1 = User::getInstance(1);
$u1->printUser();
echo "<br/>";
//同一個uid擁有一個user對象；所以34行不再創建，而是使用30創建的對象。
$u2= User::getInstance(1);
$u2->printUser();