namespace QA_LibraryManager
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Firefox;

    [TestClass]
    public class LendingBookTests
    {
        private const string BaseUrl = "http://192.168.111.66:9966/library/danipetrova/";
        private const string Account = "danipetrova";
        private const string Password = "********";

        private IWebDriver driver;

        [TestInitialize]
        public void SetUp()
        {
            this.driver = new FirefoxDriver();
            this.driver.Manage().Window.Maximize();

            this.Login(Account, Password);
            this.CreateAuthor("John", "Tolkien", "10-May-1930");
            this.CreateBook("The Hobbit", "1111-2222-3333", "10-May-2016");
            this.CreateClient("Minka", "Pavlova", "abcd", "15-May-1970");
        }
        

        [TestMethod]
        public void CreateLend_ValidData_ShouldSucceed()
        {
            this.CreateLend("10-May-2016", "10-April-2016");
            Assert.AreEqual(true, this.driver.FindElement(By.XPath("//td[contains(.,'The Hobbit')]")).Displayed);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CreateLend_WithoutStartDate_ShouldThrowException()
        {
            this.CreateLend(null, "10-April-2016");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CreateLend_WithoutEndDate_ShouldThrowException()
        {
            this.CreateLend("10-May-2016", null);
        }


        [TestCleanup]
        public void TearDown()
        {
            this.driver.Quit();
        }


        private void Login(string account, string password)
        {
            this.driver.Navigate().GoToUrl(BaseUrl + "users/login");

            IWebElement accountField = this.driver.FindElement(By.Id("username"));
            accountField.Click();
            accountField.Clear();
            accountField.SendKeys(account);

            IWebElement passwordField = this.driver.FindElement(By.Id("password"));
            passwordField.Click();
            passwordField.Clear();
            passwordField.SendKeys(password);

            IWebElement submitButton = this.driver.FindElement(By.XPath("/html/body/div[2]/div[2]/div/form/fieldset/div[3]/div/button[2]"));
            submitButton.Click();
        }

        private void CreateAuthor(string firstName, string lastName, string birthDate)
        {
            this.driver.Navigate().GoToUrl(BaseUrl + "authors/add");

            IWebElement firstNameField = this.driver.FindElement(By.Id("inputFirstName"));
            firstNameField.Click();
            firstNameField.Clear();
            firstNameField.SendKeys(firstName);

            IWebElement lastNameField = this.driver.FindElement(By.Id("inputLastName"));
            lastNameField.Click();
            lastNameField.Clear();
            lastNameField.SendKeys(lastName);

            IWebElement birthField = this.driver.FindElement(By.Id("inputBirthDate"));
            birthField.Click();
            birthField.Clear();
            birthField.SendKeys(birthDate);

            IWebElement submitButton = this.driver.FindElement(By.XPath("//*[@id=\"addAuthorForm\"]/fieldset/div[4]/div/button[2]"));
            submitButton.Click();
        }

        private void CreateBook(string title, string isbn, string publishDate)
        {
            this.driver.Navigate().GoToUrl(BaseUrl + "books/add");

            IWebElement titleField = this.driver.FindElement(By.Id("inputTitle"));
            titleField.Click();
            titleField.Clear();
            titleField.SendKeys(title);

            IWebElement isbnField = this.driver.FindElement(By.Id("inputIsbn"));
            isbnField.Click();
            isbnField.Clear();
            isbnField.SendKeys(isbn);

            IWebElement dateField = this.driver.FindElement(By.Id("inputPublishedDate"));
            dateField.Click();
            dateField.Clear();
            dateField.SendKeys(publishDate);
            
            IWebElement author =
                this.driver.FindElement(By.Id("authors")).FindElement(By.XPath("//*[@id=\"authors\"]/option[1]"));
            author.Click();

            IWebElement submitButton = this.driver.FindElement(By.XPath("//*[@id=\"addBookForm\"]/fieldset/div[5]/div/button[2]"));
            submitButton.Click();
        }


        private void CreateClient(string firstName, string lastname, string pid, string birthDate)
        {
            this.driver.Navigate().GoToUrl(BaseUrl + "clients/add");

            IWebElement firstNameField = this.driver.FindElement(By.Id("inputFirstName"));
            firstNameField.Click();
            firstNameField.Clear();
            firstNameField.SendKeys(firstName);

            IWebElement lastNameField = this.driver.FindElement(By.Id("inputLastName"));
            lastNameField.Click();
            lastNameField.Clear();
            lastNameField.SendKeys(lastname);

            IWebElement pidField = this.driver.FindElement(By.Id("inputPid"));
            pidField.Click();
            pidField.Clear();
            pidField.SendKeys(pid);

            IWebElement birthField = this.driver.FindElement(By.Id("inputBirthDate"));
            birthField.Click();
            birthField.Clear();
            birthField.SendKeys(birthDate);

            IWebElement submitButton = this.driver.FindElement(By.XPath("//*[@id=\"addClientForm\"]/fieldset/div[5]/div/button[2]"));
            submitButton.Click();
        }

        private void CreateLend(string startDate, string endDate)
        {
            this.driver.Navigate().GoToUrl(BaseUrl + "lends/add");

            IWebElement book =
                this.driver.FindElement(By.Id("selectBook")).FindElement(By.XPath("//*[@id=\"selectBook\"]/option[1]"));
            book.Click();

            IWebElement client =
                this.driver.FindElement(By.Id("selectClient")).FindElement(By.XPath("//*[@id=\"selectClient\"]/option[1]"));
            client.Click();

            IWebElement startDateField = this.driver.FindElement(By.Id("inputStartDate"));
            startDateField.Click();
            startDateField.Clear();
            startDateField.SendKeys(startDate);

            IWebElement endDateField = this.driver.FindElement(By.Id("inputReturnDate"));
            endDateField.Click();
            endDateField.Clear();
            endDateField.SendKeys(endDate);
            
            IWebElement submitButton = this.driver.FindElement(By.XPath("//*[@id=\"addLendForm\"]/fieldset/div[5]/div/button[2]"));
            submitButton.Click();
        }

        //HOW TO SELECT FROM DROPDOWN

        // select the drop down list
        //var education = driver.FindElement(By.Name("education"));
        //create select element object 
        //var selectElement = new SelectElement(education);

        //select by value
        //selectElement.SelectByValue("Jr.High");
        // select by text
        //selectElement.SelectByText("HighSchool");
    }
}
