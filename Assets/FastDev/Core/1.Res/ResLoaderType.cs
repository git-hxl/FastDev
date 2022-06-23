namespace FastDev
{
    public enum ResLoaderType
    {
        FromEditor,//从编辑器目录直接读取
        FromStreamingAssets,//从流文件夹中读取
        FromPersistentPath,//从外部目录读取
    }
}