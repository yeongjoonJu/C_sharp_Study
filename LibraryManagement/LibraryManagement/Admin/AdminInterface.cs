﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Threading.Tasks;

namespace LibraryManagement.Admin
{
    class AdminInterface
    {
        UI.ScreenUI drawer;
        Library.LibrarySystem system;
        Data.MemberManagement membermanager;
        UI.KeyInput inputProcessor;

        public AdminInterface(Library.LibrarySystem system, Data.MemberManagement membermanager)
        {
            
            drawer = new UI.ScreenUI();
            this.system = system;        
            this.membermanager = membermanager;
            inputProcessor = new UI.KeyInput();

            // 관리자 메뉴 메인 flow
            AdminMain();
        }

        public void AdminMain()
        {
            // 메뉴 선택에 따라 함수를 실행한다.
            while (true)
            {
                switch (drawer.AdminMenuScreen())
                {
                    case 1:
                        MemberManagement();
                        break;
                    case 2:
                        BookManagement();
                        break;
                    case 3:
                        return;
                }
            }
        }

        // 사용자 목록을 관리한다.
        public void MemberManagement()
        {
            ArrayList searchResult;
            while (true)
            {
                switch (drawer.MemberManagementScreen())
                {
                    case 1:
                        new Member.Register(membermanager);
                        break;
                    case 2:
                        Console.Write("\n   검색할 학번 > ");
                        searchResult = membermanager.SearchBy((int)Data.MemberManagement.Format.StudentNoFormat, inputProcessor.ReadAndCheckString(8, 18, 17, 2, true));
                        Console.Clear();
                        membermanager.ModifyMember((Data.Member)searchResult[0]);
                        break;
                    case 3:
                        Console.Write("\n   검색할 학번 > ");
                        searchResult = membermanager.SearchBy((int)Data.MemberManagement.Format.StudentNoFormat, inputProcessor.ReadAndCheckString(8, 18, 17, 2, true));
                        Console.Clear();
                        if (searchResult.Count != 0)
                        {
                            membermanager.Delete(searchResult[0]);
                            Console.WriteLine("\n   삭제되었습니다.");
                            inputProcessor.PressAnyKey();
                            Console.Clear();
                        }
                        else {
                            Console.WriteLine("\n   삭제 실패하였습니다.");
                            inputProcessor.PressAnyKey();
                            Console.Clear();
                        }
                        Console.Clear();
                        break;
                    case 4:
                        membermanager.PrintMemberList();
                        break;
                    case 5:
                        return;
                }
            }
        }

        // 서적을 관리한다.
        public void BookManagement()
        {
            ArrayList searchResult;
            int index;

            while (true)
            {
                switch (drawer.BookManagementScreen())
                {
                    case 1:
                        system.InsertBook(drawer.AddBookScreen());
                        break;
                    case 2:
                        searchResult = system.SearchBook();
                        if (searchResult.Count == 0)
                        {
                            Console.Clear();
                            Console.WriteLine("\n검색 결과가 없습니다.");
                            inputProcessor.PressAnyKey();
                            Console.Clear();
                            break;
                        }
                        index = drawer.PrintBookList(searchResult) - 1;
                        if(index != searchResult.Count)
                            system.DeleteBook((Data.Book)searchResult[index]);
                        break;
                    case 3:
                        if (system.SearchAndModifyBook())
                            Console.WriteLine("\n성공적으로 수정하였습니다.");
                        else
                            Console.WriteLine("\n수정 실패하였습니다.");
                        inputProcessor.PressAnyKey();
                        Console.Clear();
                        break;
                    case 4:
                        system.PrintAllBookList();
                        break;
                    case 5:
                        return;
                }
            }
        }
    }
}
