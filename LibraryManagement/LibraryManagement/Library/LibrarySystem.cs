﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace LibraryManagement.Library
{
    class LibrarySystem
    {
        int authority;  // 0 : 관리자 권한, 1 : 사용자 권한
        UI.ScreenUI drawer = new UI.ScreenUI();
        UI.KeyInput inputProcessor = new UI.KeyInput();
        ArrayList rentalHistoryList;
        Data.BookManagement bookKeepList;

        public LibrarySystem() {
            rentalHistoryList = new ArrayList();
            bookKeepList = new Data.BookManagement();
        }

        public void SetAuthority(int authority) {
            this.authority = authority;
        }

        public void PrintAllBookList() {
            bookKeepList.PrintBookList();
        }

        public void Rental(Data.Book book) {
            if (book.Rental)
                Console.WriteLine("현재 선택하신 책은 대출 중입니다.");
            else {
                RentalHistory rentalBook = new RentalHistory(book);
                rentalHistoryList.Add(rentalBook);
                Console.WriteLine("반납 기한은 " + rentalBook.getDueDay() + "까지 입니다.");
            }

        }

        public void InsertBook(Data.Book newBook)
        {
            if (bookKeepList.IsThereBook(newBook.Name, newBook.Writer)) {
                Console.Write("이미 등록된 책입니다.");
                inputProcessor.PressAnyKey();
            }
            else
            {
                // 도서 번호를 만든다.
                newBook.BookNo = "";
                for (int i=0; i< 6 - (bookKeepList.Count()+"").Length; i++)
                    newBook.BookNo += "0";
                newBook.BookNo += (bookKeepList.Count() + "");

                // 책 리스트에 추가한다.
                bookKeepList.Insert(newBook);
                Console.Write("\n책이 성공적으로 등록되었습니다.");
                inputProcessor.PressAnyKey();
            }
        }

        public void DeleteBook(Data.Book deletedBook)
        {
            bookKeepList.Delete(deletedBook);
            Console.Write("\n책이 성공적으로 삭제되었습니다.");
            inputProcessor.PressAnyKey();
        }

        public ArrayList SearchBook()
        {
            string searchItem;
            switch(drawer.BookSearchScreen())
            {
                case 1:
                    Console.Write("\n    검색할 도서명 > ");
                    searchItem = inputProcessor.ReadAndCheckString(10, 18, 20, 2, true);
                    return bookKeepList.SearchBy((int)Data.BookManagement.Format.NameFormat, searchItem);
                case 2:
                    Console.Write("\n    검색할 출판사 > ");
                    searchItem = inputProcessor.ReadAndCheckString(10, 18, 20, 2, true);
                    return bookKeepList.SearchBy((int)Data.BookManagement.Format.CompanyFormat, searchItem);
                case 3:
                    Console.Write("\n      검색할 저자 > ");
                    searchItem = inputProcessor.ReadAndCheckString(10, 18, 20, 2, true);
                    return bookKeepList.SearchBy((int)Data.BookManagement.Format.WriterFormat, searchItem);
            }
            return null;
        }

        public bool SearchAndModifyBook()
        {
            string searchItem;
            int format = drawer.BookSearchScreen();
            ArrayList searchResult = null;
            int index;

            switch (format)
            {
                case 1:
                    Console.Write("\n    검색할 도서명 > ");
                    searchItem = inputProcessor.ReadAndCheckString(10, 18, 20, 2, true);
                    searchResult = bookKeepList.SearchBy((int)Data.BookManagement.Format.NameFormat, searchItem);
                    break;
                case 2:
                    Console.Write("\n    검색할 출판사 > ");
                    searchItem = inputProcessor.ReadAndCheckString(10, 18, 20, 2, true);
                    searchResult = bookKeepList.SearchBy((int)Data.BookManagement.Format.CompanyFormat, searchItem);
                    break;
                case 3:
                    Console.Write("\n      검색할 저자 > ");
                    searchItem = inputProcessor.ReadAndCheckString(10, 18, 20, 2, true);
                    searchResult = bookKeepList.SearchBy((int)Data.BookManagement.Format.WriterFormat, searchItem);
                    break;
                default:
                    searchResult = new ArrayList();
                    break;
            }
            
            if (searchResult.Count == 0) {
                Console.Clear();
                Console.WriteLine("\n검색 결과가 없습니다.");
                inputProcessor.PressAnyKey();
                Console.Clear();
                return false;
            }

            index = drawer.PrintBookList(searchResult) - 1;
            Console.Write("\n수정 사항 입력 > ");
            ModifyBook((Data.Book)searchResult[index], format-1, inputProcessor.ReadAndCheckString(18, 18, 17, 1, false));
            return true;
        }

        public void ModifyBook(Data.Book modifiedBook, int format, string content)
        {
            switch (format)
            {
                case (int)Data.BookManagement.Format.NameFormat:
                    modifiedBook.Name = content;
                    break;
                case (int)Data.BookManagement.Format.CompanyFormat:
                    modifiedBook.Company = content;
                    break;
                case (int)Data.BookManagement.Format.WriterFormat:
                    modifiedBook.Writer = content;
                    break;
            }
        }

        public void Return()
        {
            
        }

        public void UserMyPage()
        {

        }
    }
}
