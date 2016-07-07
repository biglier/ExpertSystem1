using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExpertTest
{
    public partial class FormMain : Form
    {
        ExpDataContext MyContext = new ExpDataContext();
        List <PropertyQuestion> Questions = new List <PropertyQuestion>();
        List<Property> QuestionAnswers = new List<Property>();
        float GlobalWeight = new float();
        float AvgWeight = new float();
        float CurrentSummaryWeight = new float();
        float CurrentWeight = new float(); 
        List<UserAnswer> UserAnswers= new  List<UserAnswer>();
        PropertyQuestion CurrentQuestion = new PropertyQuestion();
        List<BookVariant> Books = new List<BookVariant>();
        int RunTimeCount = 0;
        bool Result = false;
        ExpertStatistic AllSystemStatistics = new ExpertStatistic();
        public FormMain()
        {
            InitializeComponent();
            LoadQuestions();
        }

        private void buttonBegin_Click(object sender, EventArgs e)
        {
            CurrentSummaryWeight = 0;
            buttonAnswer.Enabled = true;
            if (Books.Count > 0)
            {
                if (Result == true)
                {
                    SavingStatistics();
                }
                Result = false;
                foreach (var b in Books)
                {
                    b.Result = 0;
                }
            }
            else
            {
                var books = from b in MyContext.Books
                            select b.Id;
                foreach (var b in books)
                {
                    var bookVariant = new BookVariant(b, 0);
                    Books.Add(bookVariant);
                }
            }
            GoNextQuestion(20);
        }

        private  void LoadQuestions()
        {
            Questions = (from q in MyContext.PropertyQuestions
                            select q).ToList();
            float SumWeight = new float();
            AnswerWeight weight = new AnswerWeight();
            foreach (var q in Questions)
            {
                weight = QuestionWeight(q);                
                SumWeight = SumWeight+weight.Weight;
            }
            GlobalWeight = SumWeight;
            AvgWeight = SumWeight / Questions.Count;
        }

        private void LoadAnswers(PropertyQuestion question)
        {
            QuestionAnswers.Clear();
            QuestionAnswers = (from p in MyContext.Properties
                              where p.Name == question.PropertyName && p.Value!=null
                              select p).ToList();
            var variants = (from q in QuestionAnswers
                            select q.Value).ToList();
            AnswelistBox.DataSource= variants;
            
        }

        private AnswerWeight QuestionWeight(PropertyQuestion question)
        {
            var weight = (from w in MyContext.AnswerWeights
                      where w.QuestionId == question.Id
                      select w).Single();
            return weight;
        }

        private void SetAnswerList()
        {
            QuestionAnswers.Sort();
            AnswelistBox.DataSource = QuestionAnswers;
        }

        private PropertyQuestion GetQuestion(int id)
        {
            try
            {
                var question = (from q in Questions
                               where q.Id == id
                               select q).Single();
                return question;
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        private void FormMain_Load(object sender, EventArgs e)
        {

        }

        private void buttonAnswer_Click(object sender, EventArgs e)
        {
            var currentProperty = GetProperty(AnswelistBox.SelectedItem.ToString());
            BooksWeightCalculation(currentProperty);
            CurrentSummaryWeight =CurrentSummaryWeight+ CurrentWeight;
            UserAnswer userAnswer = new UserAnswer(currentProperty.Id,CurrentQuestion.Id);
            var newAnswers = UserAnswers.Find(u => u.QuestionId == userAnswer.QuestionId);
            UserAnswers.Add(userAnswer);
            if (Check() == true)
            {
                buttonBooks.Enabled = true;
                if (currentProperty.NextQuestion != null)
                    GoNextQuestion((int)currentProperty.NextQuestion);
                else buttonAnswer.Enabled = false;
            }
            else
            {
                if (currentProperty.NextQuestion == null)
                {
                    MessageBox.Show("Недостатньо інформації для прийняття рішення.");
                    buttonList.Enabled = true;
                }
                else
                {
                    GoNextQuestion((int)currentProperty.NextQuestion);
                    buttonAnswer.Enabled = true;
                }
            }
        }
        
        private Boolean Check()
        {
            if (CurrentSummaryWeight > GlobalWeight * 0.75) return true;
            else return false;
        }

        private Property GetProperty(string value)
        {
            var property = from p in QuestionAnswers
                           where p.Value == value
                           select p;
            Property returnProperty = new Property();
            foreach (var p in property)
            {
                returnProperty = p;
            }
            return returnProperty;
        }

        private Book GetBook(int id)
        {
            var booksQuery = (from b in MyContext.Books
                             where b.Id == id
                             select b).Single();
            return booksQuery;
        }

        private void ViewBooks()
        {
            List<string> books = new List<string>();
            var bookquery = from b in Books
                            where b.Result >= CurrentSummaryWeight * 0.5
                            select b;
            foreach(var b in bookquery)
            {
                var name =(from book in MyContext.Books
                           where book.Id == b.Id
                           select book.Name).Single();
                float percent = b.Result / CurrentSummaryWeight*100;
                books.Add(percent.ToString()+"%       "+name);
            }
            AnswelistBox.DataSource = books;
        }

        private void buttonBooks_Click(object sender, EventArgs e)
        {
            ViewBooks();
            Result = true;
            RunTimeCount++;
        }

        private void buttonNoAnswer_Click(object sender, EventArgs e)
        {
            CurrentSummaryWeight = +CurrentWeight;
            UserAnswer userAnswer = new UserAnswer(-1, CurrentQuestion.Id);
            UserAnswers.Add(userAnswer);
            Property property = new Property();
            foreach(var q in AnswelistBox.Items)
            {
                property = GetProperty((string)q);
                if (checkQuestion(property.Id) == false)
                {
                    GoNextQuestion((int)property.NextQuestion);
                    break;
                }
            }
        }

        private Boolean checkQuestion(int id)
        {
            try
            {
                var question = (from q in UserAnswers
                                where q.QuestionId == id
                                select q).Single();
                return true;
            }
            catch { return false; }
        }

        private void GoNextQuestion(int questionId)
        {
            var question = GetQuestion(questionId);
            if (question != null)
            {
                labelQuestion.Text = question.Question;
                LoadAnswers(question);
                AnswerWeight weight = QuestionWeight(question);
                CurrentWeight = weight.Weight;
                CurrentQuestion = question;
            }
        }

        private void BooksWeightCalculation(Property currentProperty)
        {
            List<int> varBooks = new List<int>();
            var property = from p in MyContext.Properties
                           where p.Value == currentProperty.Value
                              && p.Name == currentProperty.Name
                           select p.BookId;
            foreach (var p in property)
            {
                varBooks.Add((int)p);
            }
            foreach(var b in varBooks)
            {
             var changeWeight = (from book in Books
                                where book.Id==b
                                select book).Single();
                changeWeight.Result = changeWeight.Result + CurrentWeight;
            }
        }

        private void buttonList_Click(object sender, EventArgs e)
        {
            buttonAnswer.Enabled = false;
            List<string> questionList = new List<string>();
            var unAnswered = from u in UserAnswers
                             where u.PropertyId == -1
                             select u.QuestionId;
            foreach (var u in unAnswered) 
                {
                questionList.Add(GetQuestion(u).Question);
                }
            buttonPrevious.Enabled = true;
            AnswelistBox.DataSource = questionList;
        }

        private void buttonPrevious_Click(object sender, EventArgs e)
        {
            var question = (from q in Questions
                            where q.Question == AnswelistBox.SelectedItem.ToString()
                            select q).Single();
            GoNextQuestion(question.Id);
            buttonAnswer.Enabled = true;
            buttonPrevious.Enabled = false;
        }

        private void SavingStatistics()
        {
            RemovingUnAnswered();
            SavingUserStatistic();
            SavingQuestionStatistic();
            SavingPropertyStatistic();
        }

        private void SavingQuestionStatistic()
        {
            foreach (var aq in UserAnswers)
            {
                var s = (from w in MyContext.AnswerWeights
                                       where aq.QuestionId == w.QuestionId
                                       select w).Single();
                s.Counter++;
                var calWeight = (float)s.Counter/(float)AllSystemStatistics.Count*(float)s.DefaultWeight;
                s.Weight =calWeight;
            }
            try
            {
                MyContext.SubmitChanges();
            }
            catch
            {
                MessageBox.Show("Не вийшло записати статистику питання");
            }
        }

        private void RemovingUnAnswered()
        {
            var removingList = UserAnswers.FindAll(u => u.PropertyId == -1);
            foreach (var r in removingList)
            {
                UserAnswers.Remove(r);
            }
        }

        private void SavingUserStatistic()
        {
            try
            {
                AllSystemStatistics = (from ss in MyContext.ExpertStatistics
                                       where ss.UserId == null
                                       select ss).Single();
                AllSystemStatistics.Count++;
            }
            catch { MessageBox.Show("Запис головної статистики не виконано"); }
            try
            {
                var userStatistics = (from us in MyContext.ExpertStatistics
                                      where (int)us.UserId == UserSession.userId
                                      select us).Single();
                userStatistics.Count++;
            }
            catch
            {
                ExpertStatistic newUserStatistic = new ExpertStatistic()
                {
                    UserId = UserSession.userId,
                    Count = 1
                };
                MyContext.ExpertStatistics.InsertOnSubmit(newUserStatistic);
            }
            try
            {
                MyContext.SubmitChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void SavingPropertyStatistic()
        {
            List<Weight> weightOnInsert = new List<Weight>();
            foreach (var answer in UserAnswers)
            {
                var property = (from p in MyContext.Properties
                                where p.Id == answer.PropertyId
                                select p).Single();
                try
                {
                    var weight = (from w in MyContext.Weights
                                  where w.PropertyName == property.Name
                                     && w.PropertyValue == property.Value
                                     && w.UserId == UserSession.userId
                                  select w).Single();
                    weight.Weight1++;
                }
                catch
                {
                    Weight weight = new Weight()
                    {
                        UserId = UserSession.userId,
                        PropertyName = property.Name,
                        PropertyValue = property.Value,
                        Weight1 = 1
                    };
                    weightOnInsert.Add(weight);
                }
            }
            try
            {

                MyContext.Weights.InsertAllOnSubmit(weightOnInsert);
                MyContext.SubmitChanges();
            }
            catch
            {
                MessageBox.Show("Статистика властивостей не записана");
            }
        }
        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
