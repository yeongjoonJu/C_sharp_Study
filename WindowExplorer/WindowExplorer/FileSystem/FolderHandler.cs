﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WindowExplorer.FileSystem
{
    class FolderHandler
    {
        public FolderHandler() { }

        /// <summary>
        /// 디렉터리이면서 탐색기에 나타나지 않는 폴더인지 아닌지 확인
        /// </summary>
        /// <param name="info"> 파일 및 디렉터리 정보 객체 </param>
        /// <returns>  디렉터리이면서 탐색기에 나타나지 않는 폴더가 아니면 true를 그렇지 않으면 false </returns>
        public bool IsDirectory(DirectoryInfo info)
        {
            if (info.Attributes.HasFlag(FileAttributes.Directory) && !info.Attributes.HasFlag(FileAttributes.NotContentIndexed))
                return true;
            return false;
        }

        /// <summary>
        /// 디렉터리 목록을 반환
        /// </summary>
        /// <param name="parentPath"> 이 경로 밑의 폴더 목록을 반환 </param>
        /// <returns> 디렉터리 목록 </returns>
        public List<DirectoryInfo> GetDirectoryList(string parentPath)
        {
            List<DirectoryInfo> directoriesList = new List<DirectoryInfo>();
            string[] entries = Directory.GetFileSystemEntries(parentPath);

            foreach (string entry in entries)
            {
                DirectoryInfo info = new DirectoryInfo(entry);
                if (IsDirectory(info))
                    directoriesList.Add(info);
            }
            return directoriesList;
        }

        public List<string> GetDirectoryNameList(List<DirectoryInfo> directories)
        {
            List<string> nameList = new List<string>();
            foreach (DirectoryInfo info in directories)
                nameList.Add(info.Name);
            return nameList;
        }
    }
}