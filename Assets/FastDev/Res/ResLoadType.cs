namespace FastDev.Res
{
    public enum ResLoadType
    {
        FromEditor,//从编辑器目录直接读取
        FromStreamingAssets,//从流文件夹中读取
        FromPersistentPath,//从外部目录读取
    }
}