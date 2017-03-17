namespace SemVeyor.Storage
{
	public class FileStoreOptions
	{
		public string Path { get; set; }

		public FileStoreOptions()
		{
			Path = "history.lsj";
		}
	}
}
