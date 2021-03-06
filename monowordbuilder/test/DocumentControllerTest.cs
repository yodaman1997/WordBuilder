//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.4200
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using NUnit.Framework;
using System;
using Whee.WordBuilder.Model;
using Whee.WordBuilder.Controller;
using Whee.WordBuilder.UIHelpers;
using Whee.WordBuilder.Helpers;
using NMock2;

namespace test
{
    [TestFixture()]
    public class DocumentControllerTest
    {

        [SetUp()]
        public void Setup()
        {
            m_Mockery = new Mockery();
            m_Document = new Document();
            m_TextViewHelper = m_Mockery.NewMock<ITextViewHelper>();
            m_FileSystem = m_Mockery.NewMock<IFileSystem>();
            m_FileDialogHelper = m_Mockery.NewMock<IFileDialogHelper>();
            m_WarningViewHelper = m_Mockery.NewMock<IWarningViewHelper>();

            Expect.Once.On(m_TextViewHelper).EventAdd("BufferChanged", Is.Anything);
            Expect.Once.On(m_WarningViewHelper).EventAdd("WarningActivated", Is.Anything);
            m_DocumentController = new DocumentController(m_WarningViewHelper, m_FileSystem, m_FileDialogHelper, m_TextViewHelper, m_Document);
        }

        private Mockery m_Mockery;
        private Document m_Document;
        private DocumentController m_DocumentController;
        private ITextViewHelper m_TextViewHelper;
        private IFileSystem m_FileSystem;
        private IWarningViewHelper m_WarningViewHelper;
        private IFileDialogHelper m_FileDialogHelper;

        [Test()]
        public void TestConstructor()
        {
            Assert.IsNotNull(m_DocumentController);
        }

        [Test()]
        public void TestNew()
        {
            SetText("tokens abc a b c");
            m_Mockery.VerifyAllExpectationsHaveBeenMet();

            Expect.Once.On(m_FileDialogHelper).Method("ShowSaveCheckDialog").Will(Return.Value(SaveCheckDialogResult.NoSave));
            Expect.Never.On(m_TextViewHelper).Method("OnDocumentChanged");
            NewDocument();
            Assert.IsEmpty(m_Document.Text);

            m_Mockery.VerifyAllExpectationsHaveBeenMet();
        }

        [Test()]
        public void TestTransferTextFromModel()
        {
            SetText("tokens abc a b c");
            SetText("tokens def d e f");
            m_Mockery.VerifyAllExpectationsHaveBeenMet();
        }

        [Test()]
        public void TestTransferTextToModel()
        {
            SetText("tokens abc a b c");

            Expect.Never.On(m_TextViewHelper).Method("OnDocumentChanged");
            m_DocumentController.OnTextViewChanged(m_TextViewHelper, "tokens abcd a b c d");

            Assert.AreEqual("tokens abcd a b c d", m_Document.Text);
            m_Mockery.VerifyAllExpectationsHaveBeenMet();
        }

        [Test()]
        public void TestSaveWithFileName()
        {
            SetText("tokens abc a b c");
            m_Document.FileName = @"c:\abc.wordo";

            Expect.Once.On(m_FileSystem).Method("WriteAllText").With(@"c:\abc.wordo", "tokens abc a b c");

            m_DocumentController.Save();

            m_Mockery.VerifyAllExpectationsHaveBeenMet();
        }

        [Test()]
        public void TestSaveWithoutFileName()
        {
            SetText("tokens abc a b c");

            Expect.Once.On(m_FileDialogHelper).Method("ShowSaveDialog").Will(Return.Value(@"c:\abc.wordo"));

            Expect.Once.On(m_FileSystem).Method("WriteAllText").With(@"c:\abc.wordo", "tokens abc a b c");

            m_DocumentController.Save();

            m_Mockery.VerifyAllExpectationsHaveBeenMet();

            Assert.AreEqual(@"c:\abc.wordo", m_Document.FileName);
        }

        [Test()]
        public void TestSaveWithoutFileNameCanceled()
        {
            SetText("tokens abc a b c");

            Expect.Once.On(m_FileDialogHelper).Method("ShowSaveDialog").Will(Return.Value(null));

            Expect.Never.On(m_FileSystem).Method("WriteAllText");

            m_DocumentController.Save();

            m_Mockery.VerifyAllExpectationsHaveBeenMet();
            Assert.IsNull(m_Document.FileName);
        }

        [Test()]
        public void TestSaveAs()
        {
            SetText("tokens abc a b c");
            m_Document.FileName = @"c:\abc.wordo";

            Expect.Once.On(m_FileDialogHelper).Method("ShowSaveDialog").Will(Return.Value(@"c:\abc2.wordo"));

            Expect.Once.On(m_FileSystem).Method("WriteAllText").With(@"c:\abc2.wordo", "tokens abc a b c");

            m_DocumentController.SaveAs();

            m_Mockery.VerifyAllExpectationsHaveBeenMet();

            Assert.AreEqual(@"c:\abc2.wordo", m_Document.FileName);
        }

        [Test()]
        public void TestSaveAsCanceled()
        {
            SetText("tokens abc a b c");
            m_Document.FileName = @"c:\abc.wordo";

            Expect.Once.On(m_FileDialogHelper).Method("ShowSaveDialog").Will(Return.Value(null));

            Expect.Never.On(m_FileSystem).Method("WriteAllText");

            m_DocumentController.SaveAs();

            m_Mockery.VerifyAllExpectationsHaveBeenMet();
            Assert.AreEqual(@"c:\abc.wordo", m_Document.FileName);
        }

        [Test()]
        public void TestOpen()
        {
            Expect.Once.On(m_FileDialogHelper).Method("ShowOpenDialog").Will(Return.Value(@"c:\abc.wordo"));

            Expect.Once.On(m_FileSystem).Method("ReadAllText").With(@"c:\abc.wordo").Will(Return.Value("tokens abc a b c"));

            Expect.Once.On(m_WarningViewHelper).Method("Clear");
            Expect.Once.On(m_TextViewHelper).Method("OnDocumentChanged");
            m_DocumentController.Open();

            m_Mockery.VerifyAllExpectationsHaveBeenMet();
            m_Mockery.VerifyAllExpectationsHaveBeenMet();

            Assert.AreEqual(@"c:\abc.wordo", m_Document.FileName);
        }

        [Test()]
        public void TestCheckSave()
        {
            Expect.Never.On(m_FileDialogHelper).Method("ShowSaveCheckDialog");
            Assert.AreEqual(SaveCheckDialogResult.NoSave, m_DocumentController.CheckSave());
            m_Mockery.VerifyAllExpectationsHaveBeenMet();

            SetText("tokens abc a b c");
            Expect.Once.On(m_FileDialogHelper).Method("ShowSaveCheckDialog").Will(Return.Value(SaveCheckDialogResult.Save));
            Expect.Once.On(m_FileDialogHelper).Method("ShowSaveDialog");
            Assert.AreEqual(SaveCheckDialogResult.Save, m_DocumentController.CheckSave());
            m_Mockery.VerifyAllExpectationsHaveBeenMet();

            Expect.Once.On(m_FileDialogHelper).Method("ShowSaveCheckDialog").Will(Return.Value(SaveCheckDialogResult.NoSave));
            Assert.AreEqual(SaveCheckDialogResult.NoSave, m_DocumentController.CheckSave());
            m_Mockery.VerifyAllExpectationsHaveBeenMet();

            Expect.Once.On(m_FileDialogHelper).Method("ShowSaveCheckDialog").Will(Return.Value(SaveCheckDialogResult.Cancel));
            Assert.AreEqual(SaveCheckDialogResult.Cancel, m_DocumentController.CheckSave());
            m_Mockery.VerifyAllExpectationsHaveBeenMet();
        }

        [Test()]
        public void TestNewWithDirty()
        {
            SetText("tokens abc a b c");

            Expect.Once.On(m_FileDialogHelper).Method("ShowSaveCheckDialog").Will(Return.Value(SaveCheckDialogResult.NoSave));

            NewDocument();

            m_Mockery.VerifyAllExpectationsHaveBeenMet();

            Assert.IsEmpty(m_Document.Text);
        }

        [Test()]
        public void TestNewWithDirtyCancelled()
        {
            SetText("tokens abc a b c");

            Expect.Once.On(m_FileDialogHelper).Method("ShowSaveCheckDialog").Will(Return.Value(SaveCheckDialogResult.Cancel));

            Expect.Never.On(m_TextViewHelper).Method("Clear");
            m_DocumentController.New();

            m_Mockery.VerifyAllExpectationsHaveBeenMet();

            Assert.AreEqual("tokens abc a b c", m_Document.Text);
        }

        [Test()]
        public void TestNewWithDirtySave()
        {
            SetText("tokens abc a b c");

            m_Document.FileName = @"c:\abc.wordo";

            Expect.Once.On(m_FileDialogHelper).Method("ShowSaveCheckDialog").Will(Return.Value(SaveCheckDialogResult.Save));
            Expect.Once.On(m_FileSystem).Method("WriteAllText").With(@"c:\abc.wordo", "tokens abc a b c");

            NewDocument();

            m_Mockery.VerifyAllExpectationsHaveBeenMet();
            m_Mockery.VerifyAllExpectationsHaveBeenMet();

            Assert.IsEmpty(m_Document.Text);
        }

        [Test()]
        public void TestOpenWithDirty()
        {
            SetText("tokens abc a b c");

            Expect.Once.On(m_FileDialogHelper).Method("ShowSaveCheckDialog").Will(Return.Value(SaveCheckDialogResult.NoSave));
            Expect.Once.On(m_FileDialogHelper).Method("ShowOpenDialog").Will(Return.Value(@"c:\abc.wordo"));

            Expect.Once.On(m_FileSystem).Method("ReadAllText").With(@"c:\abc.wordo").Will(Return.Value("tokens def d e f"));

            Expect.Once.On(m_WarningViewHelper).Method("Clear");
            Expect.Once.On(m_TextViewHelper).Method("OnDocumentChanged");
            m_DocumentController.Open();

            m_Mockery.VerifyAllExpectationsHaveBeenMet();

            Assert.AreEqual("tokens def d e f", m_Document.Text);
        }

        [Test()]
        public void TestOpenWithDirtyCancelled()
        {
            SetText("tokens abc a b c");

            Expect.Once.On(m_FileDialogHelper).Method("ShowSaveCheckDialog").Will(Return.Value(SaveCheckDialogResult.Cancel));

            m_DocumentController.Open();

            m_Mockery.VerifyAllExpectationsHaveBeenMet();

            Assert.AreEqual("tokens abc a b c", m_Document.Text);
        }

        [Test()]
        public void TestOpenWithDirtySave()
        {
            SetText("tokens abc a b c");

            m_Document.FileName = @"c:\abc.wordo";

            Expect.Once.On(m_FileDialogHelper).Method("ShowSaveCheckDialog").Will(Return.Value(SaveCheckDialogResult.Save));
            Expect.Once.On(m_FileSystem).Method("WriteAllText").With(@"c:\abc.wordo", "tokens abc a b c");

            Expect.Once.On(m_FileDialogHelper).Method("ShowOpenDialog").Will(Return.Value(@"c:\def.wordo"));
            Expect.Once.On(m_FileSystem).Method("ReadAllText").With(@"c:\def.wordo").Will(Return.Value("tokens def d e f"));

            Expect.Once.On(m_TextViewHelper).Method("OnDocumentChanged");
            Expect.Once.On(m_WarningViewHelper).Method("Clear");
            m_DocumentController.Open();

            m_Mockery.VerifyAllExpectationsHaveBeenMet();

            Assert.AreEqual("tokens def d e f", m_Document.Text);
        }

        [Test()]
        public void TestCompile()
        {
            SetText("rule root {\n  literal a\n}\n");

            Expect.Once.On(m_WarningViewHelper).Method("Clear");
            Expect.Once.On(m_WarningViewHelper).GetProperty("HasWarnings").Will(Return.Value(false));
            Project project = m_DocumentController.Compile();

            Assert.IsNotNull(project);
            Assert.IsNotNull(project.Rules.GetRuleByName("root"));
            Assert.IsEmpty(project.Warnings);
        }

        [Test()]
        public void TestWarnings()
        {
            SetText("rule root {\n  token a\n}\n");

            Expect.Once.On(m_WarningViewHelper).Method("Clear");
            Expect.Once.On(m_WarningViewHelper).Method("AddWarning");
            Expect.Once.On(m_WarningViewHelper).GetProperty("HasWarnings").Will(Return.Value(true));
            Project project = m_DocumentController.Compile();

            Assert.IsNull(project);

            m_Mockery.VerifyAllExpectationsHaveBeenMet();
        }

        [Test()]
        public void TestGotoIndex()
        {
            Expect.Once.On(m_TextViewHelper).Method("GotoIndex").With(2);

            m_DocumentController.GotoIndex(2);

            m_Mockery.VerifyAllExpectationsHaveBeenMet();
        }

        private void SetText(string text)
        {
            Expect.Once.On(m_TextViewHelper).Method("OnDocumentChanged");
            Expect.Once.On(m_WarningViewHelper).Method("Clear");
            m_Document.Text = text;
        }

        private void NewDocument()
        {
            Expect.Once.On(m_TextViewHelper).Method("Clear");
            m_DocumentController.New();
        }
    }
}
