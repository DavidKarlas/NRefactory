﻿// 
// ConvertConditionalTernaryToNullCoalescingIssueTests.cs
//  
// Author:
//       Mike Krüger <mkrueger@xamarin.com>
// 
// Copyright (c) 2012 Xamarin Inc. (http://xamarin.com)
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using NUnit.Framework;
using ICSharpCode.NRefactory.CSharp.Refactoring;
using ICSharpCode.NRefactory.CSharp.CodeActions;

namespace ICSharpCode.NRefactory.CSharp.CodeIssues
{
	[TestFixture]
	public class ConvertConditionalTernaryToNullCoalescingIssueTests : InspectionActionTestBase
	{
		[Test]
		public void TestInspectorCase1 ()
		{
			var input = @"class Foo
{
	void Bar (string str)
	{
		string c = str != null ? str : ""default"";
	}
}";
			TestRefactoringContext context;
			var issues = GetIssues (new ConvertConditionalTernaryToNullCoalescingIssue (), input, out context);
			Assert.AreEqual (1, issues.Count);

			CheckFix (context, issues [0], @"class Foo
{
	void Bar (string str)
	{
		string c = str ?? ""default"";
	}
}");

		}

		[Test]
		public void TestInspectorCase2 ()
		{
			var input = @"class Foo
{
	void Bar (string str)
	{
		string c = null != str ? str : ""default"";
	}
}";
			TestRefactoringContext context;
			var issues = GetIssues (new ConvertConditionalTernaryToNullCoalescingIssue (), input, out context);
			Assert.AreEqual (1, issues.Count);
			
			CheckFix (context, issues [0], @"class Foo
{
	void Bar (string str)
	{
		string c = str ?? ""default"";
	}
}");

		}

		[Test]
		public void TestInspectorCase3 ()
		{
			var input = @"class Foo
{
	void Bar (string str)
	{
		string c = null == str ? ""default"" : str;
	}
}";
			TestRefactoringContext context;
			var issues = GetIssues (new ConvertConditionalTernaryToNullCoalescingIssue (), input, out context);
			Assert.AreEqual (1, issues.Count);
			
			CheckFix (context, issues [0], @"class Foo
{
	void Bar (string str)
	{
		string c = str ?? ""default"";
	}
}");
		}

		[Test]
		public void TestInspectorCase4 ()
		{
			var input = @"class Foo
{
	void Bar (string str)
	{
		string c = str == null ? ""default"" : str;
	}
}";
			TestRefactoringContext context;
			var issues = GetIssues (new ConvertConditionalTernaryToNullCoalescingIssue (), input, out context);
			Assert.AreEqual (1, issues.Count);
			
			CheckFix (context, issues [0], @"class Foo
{
	void Bar (string str)
	{
		string c = str ?? ""default"";
	}
}");
		}

        [Test]
        public void TestDisable()
        {
            var input = @"class Foo
{
	void Bar (string str)
	{
// ReSharper disable once ConvertConditionalTernaryToNullCoalescing
		string c = str != null ? str : ""default"";
	}
}";
            TestRefactoringContext context;
            var issues = GetIssues(new ConvertConditionalTernaryToNullCoalescingIssue(), input, out context);
            Assert.AreEqual(0, issues.Count);
        }
	}
}

