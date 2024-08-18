using FantaziaDesign.Events;
using Xunit.Abstractions;

namespace FantaziaDesign.Events.Test
{
	public delegate TReturn RefFunc<TRefArg, TReturn>(ref TRefArg arg);


	public class WeakDelegateTest
	{
		private readonly ITestOutputHelper m_output;

		public WeakDelegateTest(ITestOutputHelper output)
		{
			m_output = output;
		}

		[Theory]
		[ClassData(typeof(WeakDelegateTestData))]
		public void Test1(WeakDelegate weakDelegate, Sample sample)
		{
			var refFunc = weakDelegate.GetDelegateAs<RefFunc<int, string>>();
			int refParam1 = 0;
			sample.PublicMethodWithRefParam(ref refParam1);
			int refParam2 = 0;
			refFunc.Invoke(ref refParam2);
			Assert.Equal(refParam1, refParam2);
		}

		[Theory]
		[ClassData(typeof(WeakDelegateTestData2))]
		public void Test2(WeakDelegate weakDelegate, Sample sample)
		{
			var func = weakDelegate.GetDelegateAs<Func<string>>();
			var result = func.Invoke();
			m_output.WriteLine(result);
			Assert.Equal(sample.CallPrivateMethod(), result);
		}
	}

	public class Sample
	{
		private Guid m_id;

		public Sample()
		{
			m_id = Guid.NewGuid();
		}

		public string PublicMethod()
		{
			return m_id.ToString();
		}

		public string PublicMethodWithSingleParam(int singleParam)
		{
			return $"{m_id} ({singleParam})";
		}

		public string PublicMethodWithRefParam(ref int refParam)
		{
			refParam++;
			return $"{m_id} ({refParam})";
		}

		private string PrivateMethod()
		{
			return m_id.ToString();
		}

		public WeakDelegate GetWeakDelegateForPrivateMethod()
		{
			return WeakDelegate.FromDelegate(PrivateMethod);
		}

		public string CallPrivateMethod()
		{
			return PrivateMethod();
		}
	}

	public class WeakDelegateTestData : TheoryData<WeakDelegate, Sample>
	{
		public WeakDelegateTestData()
		{
			Sample sample1 = new Sample();
			Add(WeakDelegate.FromDelegate<RefFunc<int, string>>(sample1.PublicMethodWithRefParam), sample1);
		}
	}


	public class WeakDelegateTestData2 : TheoryData<WeakDelegate, Sample>
	{
		public WeakDelegateTestData2()
		{
			Sample sample1 = new Sample();
			Add(sample1.GetWeakDelegateForPrivateMethod(), sample1);
		}
	}

}