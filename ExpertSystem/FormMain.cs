using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExpertSystem
{
    public partial class FormMain : Form
    {
        ExpDataContext myContext = new ExpDataContext();
        ArrayList User = new ArrayList();
        ArrayList Book = new ArrayList();
        ArrayList Question = new ArrayList();
        ArrayList Property = new ArrayList();
        ArrayList PropertyDictionary = new ArrayList();
        ArrayList Weight = new ArrayList();
        int CurrentBookId = -1;
        Book CurrentBook = new Book();
        Property CurrentProperty = new Property();
        Property CurrentBookProperty = new Property();
        bool Init = false;
        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            DownloadUsers();
            List<Book> book = new List<ExpertSystem.Book>();
            List<Property> property = new List<ExpertSystem.Property>();
            
            var bdata = from b in myContext.Books
                        orderby b.Name
                        select b;
            foreach (var d in bdata)
            {
                book.Add(d);
            }
            dataGridViewBooks.DataSource = book;
            List<String> propertyname = new List<String>();
            var pdata = (from p in myContext.Properties
                         orderby p.Name
                         select p);
            foreach (var p in pdata)
            {
                property.Add(p);
                propertyname.Add(p.Name);
            }
            var distproperty = propertyname.Distinct();
            listBoxProperties.DataSource = distproperty.ToList();
            List<String> questions = new List<String>();
            var qdata = from q in myContext.PropertyQuestions
                        orderby q.Question
                        select q;
            foreach (var q in qdata)
            {
                questions.Add(q.Question);
            }
            listBoxQuestions.DataSource = questions.ToList();
            listQuestions.DataSource = questions.ToList();
            Init = true;
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {


        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tabMenu_Selected(object sender, TabControlEventArgs e)
        {

        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void dataGridViewBooks_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewBooks.RowCount != 0)
            {
                List<Property> propertiesCurrent = new List<ExpertSystem.Property>();
                CurrentBookId = Int32.Parse(dataGridViewBooks[0, dataGridViewBooks.CurrentRow.Index].Value.ToString());
                var book = from b in myContext.Books
                           where b.Id == CurrentBookId
                           select b;
                foreach (var b in book)
                {
                    CurrentBook = b;
                }
                labelBook.Text = CurrentBook.Id.ToString();
                textBoxBook.Text = CurrentBook.Name;
                if (CurrentBook.Shown == true)
                    CheckVisible.Checked = true;
                else CheckVisible.Checked = false;
                int count = new int();
                try
                {
                    var pdata = (from c in myContext.Properties
                                 where c.BookId == CurrentBookId
                                 select c).ToList();
                    propertiesCurrent = pdata;
                    dataGridViewCurrentProperties.DataSource = propertiesCurrent;
                }
                catch { }
            }
        }

        private void зберегтиToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void довыToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void dataGridViewUser_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridViewUser_SelectionChanged(object sender, EventArgs e)
        {
            var currentUser = GetUser(Int32.Parse(dataGridViewUser[0, dataGridViewUser.CurrentRow.Index].Value.ToString()));
            if (Init)
            {
                checkBoxAdmin.Checked=currentUser.Admin == true ? true : false;
                checkBoxBan.Checked =currentUser.Ban == true ? true : false;
                textBoxUser.Text = currentUser.NickName;
                labelUserId.Text = currentUser.Id.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var currentUser = GetUser(Int32.Parse(dataGridViewUser[0, dataGridViewUser.CurrentRow.Index].Value.ToString()));
            currentUser.Ban = checkBoxBan.Checked;
            currentUser.Admin = checkBoxAdmin.Checked;
            myContext.SubmitChanges();
        }

        private void buttonRedBook_Click(object sender, EventArgs e)
        {
            CurrentBook.Name = textBoxBook.Text;
            CurrentBook.Shown = CheckVisible.Checked;
            var bookUpdate = from b in myContext.Books
                             where b.Id == CurrentBook.Id
                             select b;
            foreach (var b in bookUpdate)
            {
                b.Name = CurrentBook.Name;
                b.Shown = CurrentBook.Shown;
            }
            try
            {
                myContext.SubmitChanges();
            }
            catch { }

        }

        private void buttonDelBook_Click(object sender, EventArgs e)
        {
            var delp = from p in myContext.Properties
                       where p.BookId == CurrentBook.Id
                       select p;
            List<Property> delproperties = new List<ExpertSystem.Property>();
            foreach (var p in delp)
            {
                delproperties.Add(p);
            }
            myContext.Properties.DeleteAllOnSubmit(delproperties);
            myContext.Books.DeleteOnSubmit(CurrentBook);
            myContext.SubmitChanges();
            try
            {
                List<Book> book = new List<ExpertSystem.Book>();
                var bdata = from b in myContext.Books
                            orderby b.Name
                            select b;
                foreach (var d in bdata)
                {
                    book.Add(d);
                }
                dataGridViewBooks.DataSource = book;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void buttonNewBook_Click(object sender, EventArgs e)
        {
            var searchBook = from b in myContext.Books select b;
            bool searchResult = false;
            foreach (var b in searchBook)
            {
                if (b.Name.ToLower() == textBoxBook.Text.ToLower())
                {
                    searchResult = true;
                }
            }
            if (searchResult) { MessageBox.Show("Книга уже є в системі"); }
            else
            {
                if (textBoxBook.Text.Length == 0) { MessageBox.Show("ВВедіть назву"); }
                else
                {
                    Book book = new Book
                    {
                        Name = textBoxBook.Text
                    };
                    myContext.Books.InsertOnSubmit(book);
                    myContext.SubmitChanges();
                    Book nbook = new Book();
                    var bdata = from b in myContext.Books
                                where b.Name == book.Name
                                select b;
                    foreach (var b in bdata)
                    {
                        nbook = b;
                    }
                    List<Property> propertiesBook = new List<Property>();
                    var prop = from b in myContext.Properties
                               where b.BookId == Int32.Parse(dataGridViewBooks[0, dataGridViewBooks.CurrentRow.Index].Value.ToString())
                               select b;

                    foreach (var p in prop)
                    {
                        Property newProperty = new Property
                        {
                            Name = p.Name,

                            BookId = nbook.Id
                        };
                        propertiesBook.Add(newProperty);
                    }
                    MessageBox.Show(propertiesBook.Count.ToString());
                    try
                    {
                        myContext.Properties.InsertAllOnSubmit(propertiesBook);
                        myContext.SubmitChanges();
                        List<Book> lbook = new List<ExpertSystem.Book>();
                        var bpdata = from b in myContext.Books
                                     orderby b.Name
                                     select b;
                        foreach (var d in bpdata)
                        {
                            lbook.Add(d);
                        }
                        dataGridViewBooks.DataSource = lbook;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
            }
        }

        private void buttonPropAdd_Click(object sender, EventArgs e)
        {
            var searchProperty = from p in myContext.Properties select p;
            var searchResult = false;
            foreach (var p in searchProperty)
            {
                if (p.Name.ToLower() == textBoxPropName.Text.ToLower())
                    searchResult = true;
            }
            if (searchResult) { MessageBox.Show("Властивість вже існує"); }
            else
            {
                if (textBoxPropName.Text.Length == 0) { MessageBox.Show("Введіть властивість"); }
                else
                {
                    try
                    {
                        bool visible = false;
                        var bdata = from b in myContext.Books
                                    select b;
                        List<Property> properties = new List<Property>();
                        List<Weight> weightlist = new List<ExpertSystem.Weight>();
                        foreach (var b in bdata)
                        {
                            Property nproperty = new Property()
                            {
                                Name = textBoxPropName.Text,
                                Information = textBoxInf.Text,
                                Shown = visible,
                                BookId = b.Id
                            };
                            properties.Add(nproperty);
                        }
                        myContext.Properties.InsertAllOnSubmit(properties);
                        myContext.SubmitChanges();
                        properties.Clear();
                        var pdata = from p in myContext.Properties
                                    select p;
                        foreach (var p in pdata)
                        {
                            properties.Add(p);
                        }
                        myContext.SubmitChanges();
                        List<String> propertyname = new List<String>();
                        var data = (from p in myContext.Properties
                                    orderby p.Name
                                    select p);
                        foreach (var p in data)
                        {
                            propertyname.Add(p.Name);
                        }
                        var distproperty = propertyname.Distinct();
                        listBoxProperties.DataSource = distproperty.ToList();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
            }
        }

        private void buttonPropReg_Click(object sender, EventArgs e)
        {
            try
            {
                var pdata = from p in myContext.Properties
                            where p.Name == listBoxProperties.SelectedItem.ToString()
                            select p;
                foreach (var pp in pdata)
                {
                    pp.Name = textBoxPropName.Text;
                    pp.Information = textBoxInf.Text;
                }
                myContext.SubmitChanges();
            }
            catch { };
        }


        private void dataGridViewProperties_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            int id = Int32.Parse(dataGridViewCurrentProperties[0, dataGridViewCurrentProperties.CurrentRow.Index].Value.ToString());
            var currentproperty = from c in myContext.Properties
                                  where c.Id == id
                                  select c;
            foreach (var u in currentproperty)
            {
                CurrentBookProperty = u;
            }
            textBoxPropertyValue.Text = CurrentBookProperty.Value;
            try
            {
                List<String> propdict = new List<String>();
                var data = (from p in myContext.Dicts
                            where p.Property == CurrentBookProperty.Name
                            orderby p.Value
                            select p);
                foreach (var p in data)
                {
                    propdict.Add(p.Value);
                }
                listBoxCurPropertyDict.DataSource = propdict.ToList();
            }
            catch { }
            try
            {
                var curquestion = (from q in myContext.PropertyQuestions
                                   where q.Id == CurrentBookProperty.NextQuestion
                                   select q.Question).Single();
                textBoxNextQuestion.Text = curquestion;
            }
            catch { }
            // textBoxInf.Text = CurrentBookProperty.Information;
        }

        private void buttonPropDel_Click(object sender, EventArgs e)
        {
            var currentproperty = from c in myContext.Properties
                                  where c.Name == listBoxProperties.SelectedItem.ToString()
                                  select c;
            List<Property> delproperties = new List<ExpertSystem.Property>();
            foreach (var u in currentproperty)
            {
                delproperties.Add(u);
            }
            List<Property> property = new List<ExpertSystem.Property>();
            var pdata = from p in myContext.Properties
                        select p;
            foreach (var p in pdata)
            {
                property.Add(p);
            }
            List<String> propertyname = new List<String>();
            var data = (from p in myContext.Properties
                        orderby p.Name
                        select p);
            foreach (var p in data)
            {
                property.Add(p);
                propertyname.Add(p.Name);
            }
            var distproperty = propertyname.Distinct();
            listBoxProperties.DataSource = distproperty.ToList();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                PropertyQuestion question = new PropertyQuestion
                {
                    Question = textBoxQuestion.Text,
                    PropertyName = textBoxQuestionProperty.Text
                };
                myContext.PropertyQuestions.InsertOnSubmit(question);
                myContext.SubmitChanges();
                var thisquestion = (from q in myContext.PropertyQuestions
                                    where q.Question == question.Question
                                    select q).Single();
                int weightIndex = Int32.Parse(textBoxWeight.Text);
                AnswerWeight weight = new AnswerWeight
                {
                    QuestionId = thisquestion.Id,
                    DefaultWeight = weightIndex,
                    Weight = weightIndex,
                    Counter = 0
                };
                myContext.AnswerWeights.InsertOnSubmit(weight);
                myContext.SubmitChanges();
                List<String> questions = new List<String>();
                var qdata = from q in myContext.PropertyQuestions
                            orderby q.Question
                            select q;
                foreach (var q in qdata)
                {
                    questions.Add(q.Question);
                }
                listBoxQuestions.DataSource = questions.ToList();
                listBoxQuestions.DataSource = questions.ToList();
            }
            catch
            {
                MessageBox.Show("Перевірте правильність даних");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int index = Int32.Parse(dataGridViewCurrentProperties[0, dataGridViewCurrentProperties.CurrentRow.Index].Value.ToString());
            var currentproperty = from p in myContext.Properties
                                  where p.Id == index
                                  select p;
            foreach (var p in currentproperty)
            {
                p.Value = textBoxPropertyValue.Text;
                try
                {
                    var questionId = (from q in myContext.PropertyQuestions
                                      where q.Question == textBoxNextQuestion.Text
                                      select q.Id).Single();
                    p.NextQuestion = questionId;
                }
                catch
                {
                    p.NextQuestion = null;
                }
                
               
            }

            myContext.SubmitChanges();
        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string name = listBoxProperties.SelectedItem.ToString();
            var plist = (from p in myContext.Properties
                         where p.Name == name
                         select p).First();
            textBoxPropName.Text = plist.Name;
            textBoxInf.Text = plist.Information;
            try
            {
                var question = from q in myContext.PropertyQuestions
                               where q.PropertyName == listBoxProperties.SelectedItem.ToString()
                               select q;
                foreach (var q in question)
                {
                    q.PropertyName = listBoxProperties.SelectedItem.ToString();
                }
            }
            catch { }
            try
            {
                List<String> propdict = new List<String>();
                var data = (from p in myContext.Dicts
                            where p.Property == listBoxProperties.SelectedItem.ToString()
                            orderby p.Value
                            select p);
                foreach (var p in data)
                {
                    propdict.Add(p.Value);
                }
                listBoxSIct.DataSource = propdict.ToList();
            }
            catch { }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            Dict dictelement = new Dict()
            {
                Property = listBoxProperties.SelectedItem.ToString(),
                Value = textBoxDictValue.Text
            };
            myContext.Dicts.InsertOnSubmit(dictelement);
            myContext.SubmitChanges();
            try
            {
                List<String> propdict = new List<String>();
                var data = (from p in myContext.Dicts
                            where p.Property == listBoxProperties.SelectedItem.ToString()
                            orderby p.Value
                            select p);
                foreach (var p in data)
                {
                    propdict.Add(p.Value);
                }
                listBoxSIct.DataSource = propdict.ToList();
               
            }
            catch { };
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                List<Dict> propertydictionary = new List<ExpertSystem.Dict>();
                var del = from c in myContext.Dicts
                          where c.Value == listBoxSIct.SelectedItem.ToString()
                          && c.Property == listBoxProperties.SelectedItem.ToString()
                          select c;
                foreach (var d in del)
                {
                    propertydictionary.Add(d);
                }
                myContext.Dicts.DeleteAllOnSubmit(propertydictionary);
                myContext.SubmitChanges();
                List<String> propdict = new List<String>();
                var data = (from p in myContext.Dicts
                            where p.Property == listBoxProperties.SelectedItem.ToString()
                            orderby p.Value
                            select p);
                foreach (var p in data)
                {
                    propdict.Add(p.Value);
                }
                listBoxSIct.DataSource = propdict.ToList();
            }
            catch { };
        }

        private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBoxDictValue.Text = listBoxSIct.SelectedItem.ToString();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                var search = (from c in myContext.Dicts
                              where c.Value == listBoxSIct.SelectedItem.ToString()
                              select c);
                foreach (var d in search)
                {
                    d.Value = textBoxDictValue.Text;
                }
                myContext.SubmitChanges();
                List<String> propdict = new List<String>();
                var data = (from p in myContext.Dicts
                            where p.Property == listBoxProperties.SelectedItem.ToString()
                            orderby p.Value
                            select p);
                foreach (var p in data)
                {
                    propdict.Add(p.Value);
                }
                listBoxSIct.DataSource = propdict.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void listBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var question = (from q in myContext.PropertyQuestions
                                where q.Question == listBoxQuestions.SelectedItem.ToString()
                                select q).Single();
                textBoxQuestion.Text = question.Question;
                textBoxQuestionProperty.Text = question.PropertyName;
                var qWeight = (from w in myContext.AnswerWeights
                               where w.QuestionId == question.Id
                               select w).Single();
                textBoxWeight.Text = qWeight.Weight.ToString();
                labelDefWeight.Text = qWeight.DefaultWeight.ToString();
                labelCount.Text = qWeight.Counter.ToString();
            }
            catch { MessageBox.Show("Помилка у роботы ыз базою"); }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            List<PropertyQuestion> delquestion = new List<PropertyQuestion>();
            var qdata = from q in myContext.PropertyQuestions
                        where q.Question == listBoxQuestions.SelectedItem.ToString()
                        select q;
            foreach (var q in qdata)
            {
                delquestion.Add(q);
            }
            myContext.PropertyQuestions.DeleteAllOnSubmit(delquestion);
            myContext.SubmitChanges();
            List<String> questions = new List<String>();
            qdata = from q in myContext.PropertyQuestions
                    orderby q.Question
                    select q;
            foreach (var q in qdata)
            {
                questions.Add(q.Question);
            }
            listBoxQuestions.DataSource = questions.ToList();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            textBoxQuestionProperty.Text = listBoxProperties.SelectedItem.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var qdata = (from q in myContext.PropertyQuestions
                        where q.Question == listBoxQuestions.SelectedItem.ToString()
                        select q).Single();
            qdata.Question = textBoxQuestion.Text;
            qdata.PropertyName = textBoxQuestionProperty.Text;
            AnswerWeight weight = new  AnswerWeight();
            weight=myContext.AnswerWeights.Where(w=> w.QuestionId==qdata.Id)
                                           .Select(w=>w).Single();
            int weightIndex = new int();
            try
            {
                weightIndex = Int32.Parse(textBoxWeight.Text);
                weight.DefaultWeight = weightIndex;
                if (weight.Weight == weight.DefaultWeight | weight.Weight == 0)
                    weight.Weight = weightIndex;
                else WeightCalculating(weight);

            }
            catch { };
            myContext.SubmitChanges();
            }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            textBoxPropertyValue.Text = listBoxCurPropertyDict.SelectedItem.ToString();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            textBoxNextQuestion.Text = listQuestions.SelectedItem.ToString();
        }

        public float WeightCalculating(AnswerWeight weight)
        {
            var ExpS = (from e in myContext.ExpertStatistics
                       where e.UserId ==null
                       select e.Count).Single();
            float newWeight = weight.Counter / ExpS * weight.DefaultWeight;           
            return newWeight;
        }

        public void DownloadUsers()
        {
            try
            {
                var users = (from c in myContext.Users
                             orderby c.NickName
                             select c).ToList();
                dataGridViewUser.DataSource = users;
            }
            catch
            {
                MessageBox.Show("Проблема при з'єднанні із базою");
            }   
        }

        public User GetUser(int id)
        {
            var user = (from c in myContext.Users
                               where c.Id == id
                               select c).Single();
            return user;
        }
    }
}

