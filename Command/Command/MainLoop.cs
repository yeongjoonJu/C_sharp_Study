﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Command
{
    class MainLoop
    {
        OutputProcessor output;
        Functions functions;
        List<string> cmdList;
        string currentPath;
        enum COMMAND { CMD=0, CD, DIR, CLS, HELP, COPY, MOVE, EXIT };

        public MainLoop()
        {
            string homeDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            output = new OutputProcessor();
            currentPath = homeDirectory;
            functions = new Functions(output);
            cmdList = functions.GetCmdList();
        }
                  
        public void Loop()
        {
            functions.VersionInfomation();
            string input;
            string command;
            string[] cmds;
            
            while(true)
            {
                output.PrintCurrentPath(currentPath);
                input = Console.ReadLine();
                cmds = input.Split(' ');

                // 입력된 명령어
                command = cmds[0].ToLower();

                // 명령어 목록 존재 확인
                if(cmdList.Contains(command))
                {
                    string secondParam;
                    switch (cmdList.IndexOf(command))
                    {
                        case (int)COMMAND.CMD:
                            functions.VersionInfomation();
                            break;
                        case (int)COMMAND.CD:
                            int paramPos = input.IndexOf(' ') + 1;  // 인수의 시작 인덱스
                            if (paramPos >= input.Length || paramPos == 0)
                                Console.WriteLine(currentPath);
                            else {
                                string movedPath = functions.ChangeDirectory(input.Substring(paramPos), currentPath);  // 이동된 경로를 구하면
                                if (movedPath != null) currentPath = movedPath;  // 없는 경로가 아니라면
                                else Console.WriteLine("지정된 경로를 찾을 수 없습니다."); // 없는 경로라면
                            }
                            break;
                        case (int)COMMAND.DIR:
                            if (cmds.Length == 1) functions.FileList("", currentPath);
                            else functions.FileList(cmds[1], currentPath);
                            break;
                        case (int)COMMAND.CLS:
                            Console.Clear();
                            break;
                        case (int)COMMAND.COPY:
                            // 인수 개수에 대한 예외처리
                            secondParam = Exception.ArgumentCountException(cmds);
                            if (secondParam != null)
                                functions.Copy(cmds[1], secondParam, currentPath);
                            break;
                        case (int)COMMAND.HELP:
                            if (cmds.Length == 1) functions.PrintHelp("");
                            else functions.PrintHelp(cmds[1]);
                            break;
                        case (int)COMMAND.MOVE:
                            // 인수 개수에 대한 예외처리
                            secondParam = Exception.ArgumentCountException(cmds);
                            if(secondParam != null)
                                functions.Move(cmds[1], secondParam, currentPath);
                            break;
                        case (int)COMMAND.EXIT:
                            return;
                    }
                }
                else
                    Console.WriteLine("\'" + command + "\'은(는) 내부 또는 외부 명령, 실행할 수 있는 프로그램, 또는 배치 파일이 아닙니다.");

            }
        }
    }
}
