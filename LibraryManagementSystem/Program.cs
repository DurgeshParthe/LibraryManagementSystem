// See https://aka.ms/new-console-template for more information
using System.ComponentModel.Design;
using System.Diagnostics;
using LibraryManagementSystem;
Book book = new Book();
Member member = new Member();
Transaction transaction = new Transaction();
Console.WriteLine("Hello, World!");
Console.WriteLine(" Welcome To Library Management System");


for (; ; )
{
    menu();
}


void menu()
{

    int choice;

    Console.WriteLine("Enter Your Choice \n1.Book \n2.Member \n3.Transaction");
    choice = Convert.ToInt32(Console.ReadLine());

    switch (choice)
    {
        case 1:
            bookChoice();
            break;

        case 2:
            memberChoice();
            break;

        case 3:
            transactionChoice();
            break;
    }
}


void bookChoice()
{
    Console.WriteLine("1.Add New Book\n2.Display All Book's\n3.Search Book By Title\n4.Search Book By ID\n5.Edit Book \n6.Remove Book \n7.Borrow Book");


    int choice;
    choice = Convert.ToInt32(Console.ReadLine());

    switch (choice)
    {
        case 1:
            book.AddBook();
            break;

        case 2:
            book.getAllBook();
            break;

        case 3:
            book.getBookByTitle();
            break;

        case 4:
            book.getBookById();
            break;

        case 5:
            DBSource.editBook();
            break;

        case 6:
            DBSource.removeBook();
            break;
        case 7:
            transaction.addBorrowDetails();
            break;

    }

}

void memberChoice()
{
    Console.WriteLine("1.Add New Member\n2.Display All Members \n3.Remove Member");
    int choice;
    choice = Convert.ToInt32(Console.ReadLine());

    switch (choice)
    {
        case 1:
            member.addMember();
            break;

        case 2:
            member.GetAllMember();
            break;
        case 3:
            DBSource.removeMember();
            break;
    }
}

void transactionChoice()
{
    Console.WriteLine("1.Borrow Book\n2.Return Book\n3.Display Borrowed Books");
    int choice;
    choice = Convert.ToInt32(Console.ReadLine());

    switch (choice)
    {
        case 1:
        case 7:
            transaction.addBorrowDetails();
        
            break;

        case 2:
            transaction.addReturnDetails();
            break;
        case 3:
            transaction.getBorrowedBooks();
            break;
    }
}

//PracticeAdapter.practiceA();