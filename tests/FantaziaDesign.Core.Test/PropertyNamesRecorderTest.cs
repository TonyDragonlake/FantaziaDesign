using FantaziaDesign.Core;
using Xunit.Abstractions;

namespace FantaziaDesign.Events.Test
{
	public class PropertyNamesRecorderTest
	{
		public class TestDataClass
		{
			public int MyProperty0 { get; set; }
			public int MyProperty1 { get; set; }
			public int MyProperty2 { get; set; }
			public int MyProperty3 { get; set; }
			public int MyProperty4 { get; set; }
			public int MyProperty5 { get; set; }
			public int MyProperty6 { get; set; }
			public int MyProperty7 { get; set; }
			public int MyProperty8 { get; set; }
			public int MyProperty9 { get; set; }
			public int MyProperty10 { get; set; }
			public int MyProperty11 { get; set; }
			public int MyProperty12 { get; set; }
			public int MyProperty13 { get; set; }
			public int MyProperty14 { get; set; }
			public int MyProperty15 { get; set; }
			public int MyProperty16 { get; set; }
		}

		private readonly ITestOutputHelper m_output;
		private readonly IPropertyNamesRecorder m_recorder;
		private readonly string[] actuals = new string[]
		{
			"MyProperty3",
			"MyProperty5",
			"MyProperty6",
			"MyProperty8",
			"MyProperty9",
			"MyProperty13",
			"MyProperty14",
			"MyProperty15",
		};

		public PropertyNamesRecorderTest(ITestOutputHelper output)
		{
			m_output = output;
			m_recorder = PropertyNamesRecorder<TestDataClass>.Create();

			foreach (var actual in actuals)
			{
				m_recorder.TrySetRecorder(actual, true);
			}
		}

		[Fact]
		public void TestResult_OfChanged_ShouldEquals_OfChangedUsingSkip()
		{
			var ofChangedArray = m_recorder.OfChanged().ToArray();

			Assert.Equal(ofChangedArray, actuals);
		}
	}

}