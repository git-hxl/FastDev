
# FastDev
- 1.资源加载模块
	>ResConfig：用于对比资源版本和文件哈希值  
	>ResLoadType：选择从外部目录、StreamingAssets或者编辑器目录加载资源  
	>ResManager  
	>>LoadAssetBundle //加载指定的AB包  
	>>LoadAsset<T> //加载指定类型的资源  
	>>Dispose()  
	>>...  
- 2.UI模块
	>IPanel：统一UI接口  
	>UIPanel:Ipanel：统一UI抽象类 包含常用方法 所有UI对象应继承该类  
	>UIManager：统一UI管理器  
	>>GetPanel(string path) //获取指定UI  
	>>...  
- 3.音频模块
	>AudioSetting:音量常见设置  
	>AudioType:音频类型  
	>AudioManager：统一音频管理器  
	>>PlayClip()：//首次播放会自动通过ResManager加载 后续直接从缓存池中获取  
	>>PlayClipAtPoint()  
	>>PlayMusic()  
	>>...  
- 4.消息模块  
	>MsgID:消息ID  
	>EventManager:  
	>>Register(int eventID,Action<Hashtable> action)//evnetid==msgid  
	>>Dispatch(int eventID,Hashtable hashtable)  
	>MsgManager:  
	>>Enqueue(int msgID, Hashtable hashtable)//该方法会将消息加入线程安全的队列 用于多线程消息通信  
- 5.WebRequest模块
	>WebRequestManager:  
	>>Get(string url)  
	>>GetSprite(string url)  
	>>Post(string url, WWWForm form)  
	>>Put(string url, byte[] bodyData)  
	>>Download(string url, string path, Action<float> progress)  
- 6.缓存池模块
	>Pool:  
	>PoolManager  
	>>Allocate(string assetPath)  
	>>Allocate(string assetPath, int count)  
	>>Recycle(GameObject obj)  
	>>Recycle(GameObject[] objs)  
	>>Recycle(GameObject obj, int millisecondsDelay)//延迟回收  
	>>Recycle(GameObject[] objs, int millisecondsDelay)//延迟回收  
- 7.Socket模块  
	>DataPacker：封包解包  
	>HeartBeatTool：心跳检测  
	>MiniTcpClient：Tcp  
	>MiniUdpClient: Udp  
- 8.工具
	>AssetBundleEditor：AB包打包工具  
	>SecurityUtil：加密解密工具  
	>SerializeUtil：数据序列化和反序列化  
	>代码自动生成  
	>多语言工具  
	>在编辑器中添加proto文件转C#功能  
- 9.热更  
        >ILRuntime,支持protobuf协议热更

