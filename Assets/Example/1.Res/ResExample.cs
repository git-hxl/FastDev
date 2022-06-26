
using UnityEngine;
using FastDev;
public class ResExample : MonoBehaviour
{
    // Start is called before the first frame update
    private async void Start()
    {
        //ResUpdater resUpdater = new ResUpdater("https://xxx");

        //resUpdater.onUpdateProcess += ResUpdater_onUpdateProcess;
        //resUpdater.onUpdateFailed += ResUpdater_onUpdateFailed;
        //resUpdater.onUpdateProcess += ResUpdater_onUpdateProcess1;


        ResLoader.Instance.ResLoaderType = ResLoaderType.FromEditor;
        await ResLoader.Instance.LoadAllAssetBundle();

        Instantiate(ResLoader.Instance.LoadAsset<GameObject>("","Assets/Example/1.Res/Cube.prefab"));
    }

    private void ResUpdater_onUpdateProcess1(string arg1, float arg2)
    {
        throw new System.NotImplementedException();
    }

    private void ResUpdater_onUpdateFailed()
    {
        throw new System.NotImplementedException();
    }

    private void ResUpdater_onUpdateProcess(string arg1, float arg2)
    {
        throw new System.NotImplementedException();
    }
}
