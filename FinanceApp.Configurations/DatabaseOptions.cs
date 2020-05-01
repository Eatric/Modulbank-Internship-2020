namespace FinanceApp.Core.Configurations
{
	public class DatabaseOptions
	{
		public string Host { get; set; }
		public uint Port { get; set; }
		public string Database { get; set; }
		public string User { get; set; }
		public string Password { get; set; }

		public override string ToString()
		{
			return $"host={Host};port={Port};database={Database};username={User};password={Password};";
		}
	}
}
