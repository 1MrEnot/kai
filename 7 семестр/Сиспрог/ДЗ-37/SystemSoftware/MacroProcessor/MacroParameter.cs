namespace SystemSoftware.MacroProcessor
{
	/// <summary>
	/// Параметр макроса.
	/// </summary>
	public class MacroParameter
	{
		/// <summary>
		/// Название.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Тип.
		/// </summary>
		public MacroParameterTypes Type { get; set; }

		/// <summary>
		/// Значение по умолчанию.
		/// </summary>
		public string DefaultValue { get; set; }
		public string Value { get; set; }

		public MacroParameter() { }

		public MacroParameter(string name, MacroParameterTypes type, string defaultValue = null)
		{
			Name = name;
			Type = type;
			DefaultValue = defaultValue;
			Value = defaultValue;
		}
	}

	/// <summary>
	/// Тип макропараметра.
	/// </summary>
	public enum MacroParameterTypes
	{
		Position,
		Key
	}
}
