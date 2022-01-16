using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Security;

namespace TP5
{
    public class Interview
    {
        public class Profile
        {
            public string Name;
            public string Sex = " ";
            public string Adress = " ";
            public uint Age = 0;
            public uint Size = 0;
            public List<String> Question = new List<string>();
            public List<String> Answer = new List<string>();
            public Profile(string name)
            {
                this.Name = name;
            }
        }



        public static Profile ReadProfile(string path)
        {
            Profile profile = new Profile(" ");
            string[] profileInfo = path.Split(':');
            StreamReader myReader = File.OpenText(path);
            string line;

            while ((line = myReader.ReadLine()) != null)
            {
                if (profileInfo[0] == "name")
                    profile.Name = profileInfo[1];
                else if (profileInfo[0] == "age")
                    profile.Age = UInt32.Parse(profileInfo[1]);
                else if (profileInfo[0] == "adress")
                    profile.Adress = profileInfo[1];
                else if (profileInfo[0] == "sex")
                    profile.Sex = profileInfo[1];
                else if (profileInfo[0] == "size")
                    profile.Size = UInt32.Parse(profileInfo[1]);
            }
            myReader.Close();
            return profile;
        }

        public static void ReadQuestion(List<Profile> allProfiles, string path)
        {

            StreamReader myReader = File.OpenText(path);
            string line = myReader.ReadLine();
            string question = "";
            string[] answerinfo = new string[allProfiles.Count];
            string[] profiles = new string[allProfiles.Count];
            while (!myReader.EndOfStream)
            {
                if (line.Split(':')[0] == "question")
                {
                    question = line.Split(':')[1];
                }
                foreach (Profile profile in allProfiles)
                {
                    if (answerinfo[0] == profile.Name)
                    {
                        profile.Answer.Add(answerinfo[1]);
                        profile.Question.Add(question);
                    }
                }
                line = myReader.ReadLine();
            }
            myReader.Close();
        }

        public static void PrintInformation(Profile profile)
        {
            Console.WriteLine("Name:" + profile.Name);
            Console.WriteLine("Sex:" + profile.Sex);
            Console.WriteLine("Age:" + profile.Age);
            Console.WriteLine("Size:" + profile.Size);
            Console.WriteLine("Adress:" + profile.Adress);
            for (int i = 0; i < profile.Question.Count; i++)
            {
                Console.WriteLine("Question:" + profile.Question[i]);
                if (profile.Answer[i] != null)
                {
                    Console.WriteLine("Answer:" + profile.Answer[i]);
                }
            }
        }

        public static void SaveProfile(Profile profile)
        {

            if (File.Exists(profile.Name + ".profile"))
            {
                StreamWriter myw = File.CreateText(profile.Name + ".profile");
                myw.WriteLine("Name:" + profile.Name);
                myw.WriteLine("Sex" + profile.Sex);
                myw.WriteLine("Age:" + profile.Age);
                myw.WriteLine("Adress:" + profile.Adress);
                for (int i = 0; i < profile.Question.Count; i++)
                {

                    if (profile.Question[i] != null)
                    {
                        myw.WriteLine("Question:" + profile.Question[i]);
                    }
                    if (profile.Answer[i] != null)
                    {
                        myw.WriteLine("Answer:" + profile.Answer[i]);
                    }
                }
            }

            else
            {
                StreamWriter myWriter = File.CreateText(profile.Name + ".profile");
                myWriter.WriteLine("Name:" + profile.Name);
                myWriter.WriteLine("Sex" + profile.Sex);
                myWriter.WriteLine("Age:" + profile.Age);
                myWriter.WriteLine("Adress:" + profile.Adress);
                for (int i = 0; i < profile.Question.Count; i++)
                {
                    if (profile.Question[i] != null)
                    {
                        myWriter.WriteLine("Question:" + profile.Question[i]);
                    }
                    if (profile.Answer[i] != null)
                    {
                        myWriter.WriteLine("Answer:" + profile.Answer[i]);
                    }
                }
            }
        }

        public static void CreateProfile()
        {
            Profile profile = new Profile(" ");
            Console.WriteLine("Which field ?");
            string field = Console.ReadLine();
            switch (field)
            {
                case "name":
                    profile.Name = field;
                    break;
                case "age":
                    profile.Age = UInt32.Parse(field);
                    break;
                case "adress":
                    profile.Adress = field;
                    break;
                case "sex":
                    profile.Sex = field;
                    break;
                case "question":
                    profile.Question.Add(field);
                    break;
                case "answer":
                    profile.Answer.Add(field);
                    break;
                case "exit":
                    if (profile.Name != null)
                    {
                        SaveProfile(profile);
                    }
                    else
                    {
                        Console.WriteLine("Failed to create this new Profile");
                    }
                    break;
                default:
                    Console.WriteLine("What did you mean by " + field + " ?");
                    break;
            }

        }

        public static void Interrogation()
        {
            // TODO
            throw new NotImplementedException();
        }
    }
}