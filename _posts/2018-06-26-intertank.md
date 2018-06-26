---
layout: splash
title:  "多人游戏与网络"
date:   2018-06-26 14:06:05
categories: 3D游戏编程与设计
excerpt: internet
---

* content
{:toc}

## 从[这里](https://pmlpml.github.io/unity3d-learning/13-Multiplayer-and-Networking)开始设置多人游戏

* [项目地址](https://github.com/Lyrix28/Lyrix28.github.io/tree/master/assets/UnityProject/intertank)

![Image text](https://raw.githubusercontent.com/Lyrix28/Lyrix28.github.io/master/assets/Pictures/intertank.gif)

---

### Pre

> 导入 [tanks-tutorial](https://unity3d.com/cn/learn/tutorials/s/tanks-tutorial) ，打开 _Complete-Game 场景

---

### 玩家对象联网运动

#### 1. NetworkManager 设置

> * 从菜单 Game Object -> Create Empty 添加一个新的空游戏对象。
* 在层次结构视图中选择它。
* 将对象重命名为“NetworkManager”，使用右键上下文菜单中或单击对象的名称并键入。
* 在对象的检查器窗口中，单击添加组件按钮
* 找到组件 Network -> NetworkManager 并将其添加到对象。该组件管理游戏的网络状态。
* 找到组件 Network -> NetworkManagerHUD 并将其添加到对象。该组件在您的游戏中提供了一个简单的用户界面来控制网络状态。

#### 2. 设置玩家对象预制

> * 选择 _Completed-Assets\Prefabs\CompleteTank 预制
* 在对象的检查器窗口中，单击添加组件按钮
* 将组件 Network -> NetworkIdentity 添加到对象。该组件用于标识服务器和客户端之间的对象。
* 将 NetworkIdentity 上的 “Local Player Authority” 复选框设置为 true。这将允许客户端控制玩家对象的移动

#### 3. 注册玩家预制

> * 在层次视图中找到 NetworkManager 对象并选择它
* 在 NetworkManager 组件面板打开 “Spawn Info” 折叠
* 找到 “Player Prefab” 插槽（可以拖入对象的属性）
* 将 CompleteTank 预制件拖入 “Player Prefab” 插槽

#### 4. 玩家对象运动（单人版）

#### 5. 测试主机（hosted）游戏

#### 6. 测试玩家对客户的移动

#### 7. 联网玩家对象的运动

> * 打开 _Completed-Assets\Scripts\Tank\TankMovement 脚本
* 添加 “using UnityEngine.Networking”
* 将 “MonoBehaviour” 更改为 “NetworkBehaviour”
* 在Update函数中添加一个 “isLocalPlayer” 检测，以便只有本地程序处理输入
* 在资源视图中找到 CompleteTank 预制并选择
* 单击 “Add Component” 按钮并添加 Networking -> NetworkTransform 组件。该组件使对象在网络中同步它的位置。

#### 8. 测试多人游戏

#### 9. 识别本地玩家对象

为了识别玩家，我们将使本地玩家的坦克变红。

> * 打开 _Completed-Assets\Scripts\Tank\TankMovement 脚本
* 添加 OnStartLocalPlayer 函数的实现来更改运行期间玩家对象的颜色。

{% highlight C# linenos %}
public override void OnStartLocalPlayer() {
	// Get all of the renderers of the tank.
	MeshRenderer[] renderers = gameObject.GetComponentsInChildren<MeshRenderer> ();

	// Go through all the renderers...
	for (int i = 0; i < renderers.Length; i++)
	{
		// ... set their material color to the color specific to this tank.
		renderers[i].material.color = Color.red;
	}
}
{% endhighlight %}

---

### 联网相互射击

#### 1. 射击子弹（未联网）

> 受重力影响

#### 2. 联网射击子弹

> * 选择 _Completed-Assets\Prefabs\CompleteShell 预制
* 在对象的检查器窗口中，单击添加组件按钮
* 将 NetworkIdentity, NetworkTransform 添加到项目符号预制
* 在子弹预制的 NetworkTransform 组件上将发送速率设置为零。子弹在射击后不会改变方向或速度，因此不需要发送移动更新。

> CompleteTank 和 CompleteShell 预制 NetworkTransform 组件 Transform Syns Mod 的都为 Sync Rigidbody 3D

> * 选择 NetworkManager 并打开 “Spawn Info” 折叠
* 用 Add 按钮添加一个新的 spawn 预制件
* 将 CompleteShell 预制件拖入新的 spawn 预制插槽
* 打开 _Completed-Assets\Scripts\Tank\TankShooting 脚本
* 通过添加 [Command] 自定义属性和“Cmd”前缀，将Fire功能更改为联网命令
* 在子弹对象上使用Network.Spawn（）

{% highlight C# linenos %}
[Command]
private void CmdFire ()
{
    // Set the fired flag so only Fire is only called once.
    m_Fired = true;

    // Create an instance of the shell and store a reference to it's rigidbody.
    Rigidbody shellInstance = Instantiate (m_Shell, m_FireTransform.position, m_FireTransform.rotation) as Rigidbody;

    // Set the shell's velocity to the launch force in the fire position's forward direction.
    shellInstance.velocity = m_CurrentLaunchForce * m_FireTransform.forward; 

	NetworkServer.Spawn (shellInstance.gameObject);

    // Change the clip to the firing clip and play it.
    m_ShootingAudio.clip = m_FireClip;
    m_ShootingAudio.Play ();

    // Reset the launch force.  This is a precaution in case of missing button events.
    m_CurrentLaunchForce = m_MinLaunchForce;
}
{% endhighlight %}

#### 3. 子弹碰撞

> * 打开 _Completed-Assets\Scripts\Shell\ShellExplosion 脚本
* 取消 OnTriggerEnter 函数中力的作用

{% highlight C# linenos %}
private void OnTriggerEnter (Collider other)
{
	//···

	// Add an explosion force.
	//targetRigidbody.AddExplosionForce (m_ExplosionForce, transform.position, m_ExplosionRadius);

	//···
}
{% endhighlight %}

---

### 玩家状态同步

#### 1. 玩家状态（非联网生命值）

> * 打开 _Completed-Assets\Scripts\Tank\TankHealth 脚本
* 将 SetHealthUI 函数改为 void OnGUI 函数，取消所有 SetHealthUI 函数的调用

{% highlight C# linenos %}
void OnGUI()
{
    // Set the slider's value appropriately.
    m_Slider.value = m_CurrentHealth;

    // Interpolate the color of the bar between the choosen colours based on the current percentage of the starting health.
    m_FillImage.color = Color.Lerp (m_ZeroHealthColor, m_FullHealthColor, m_CurrentHealth / m_StartingHealth);
}
{% endhighlight %}

#### 2. 玩家状态（网络健康）

> * 打开 _Completed-Assets\Scripts\Tank\TankHealth 脚本
* 将脚本更改为 NetworkBehaviour
* [SyncVar] 指令生命值属性具有 “服务器权限”
* 将 isServer 检查添加到 TakeDamage 中，所以它只会应用在服务器上

{% highlight C# linenos %}
public class TankHealth : NetworkBehaviour
{
	//···

	[SyncVar]
	private float m_CurrentHealth;                      // How much health the tank currently has.

	//···
}
{% endhighlight %}

#### 3. 死亡和重生

> * 打开 _Completed-Assets\Scripts\Tank\TankHealth 脚本
* 添加一个 [ClientRpc] 指示 RpcRespawn 函数具有 “本地权限”。有关更多信息，请参阅 Networked Actions。
* 当生命值达到零时，调用服务器上的 RpcRespawn 函数操作，[ClientRpc] 指令在所有客户端执行该函数。

{% highlight C# linenos %}
[ClientRpc]
void RpcRespawn()
{
	if (isLocalPlayer) {
		transform.position = Vector3.zero;
	}
}
{% endhighlight %}

### 运行

> 取消 GameManager, CameraControl 脚本

---

## 参考资料
* [第十三章、多人游戏与网络](https://pmlpml.github.io/unity3d-learning/13-Multiplayer-and-Networking)

* [Multiplayer and Networking](https://docs.unity3d.com/Manual/UNet.html)