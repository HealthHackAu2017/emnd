using System;
using System.Collections.Generic;
using Serilog;

namespace Emnd
{
    public class SurveyQuestionModel
    {
    }
        public partial class SurveyQuestion
        {
            public string QuestionVariable { get; set; }
            public string Section { get; set; }
            public string Question { get; set; }
            public double MinValue { get; set; }
            public double MaxValue { get; set; }
            public string MinText { get; set; }
            public string MaxText { get; set; }
            public double Scale { get; set; }
            public double DefaultValue { get; set; }
            public double AnswerValue { get; set; }
            public bool Answered { get; set; }
        }

        public partial class SurveySection
        {
            public string SectionVariable { get; set; }
            public string SectionName { get; set; }
            public string SectionInfo { get; set; }
            public bool SectionSkipped { get; set; }
            public bool Answered { get; set; }
            public bool NotAnswered => !Answered && !SectionSkipped;
            public string CompletionText => SectionSkipped ? "no symptoms" : (Answered ? "completed" : "");
            public List<SurveyQuestion> SectionQuestions { get; set; }
        }

    public partial class Survey
    {
        public Survey()
        {
            SurveyDate = DateTime.Now;
            SurveyGUID = Guid.NewGuid().ToString();
            ParticipantName = AppSettings.ParticipantName;
            ParticipantID = AppSettings.ParticipantId;
            Weight = AppSettings.ParticipantWeight;

            _sections = NewSections();
            _questions = NewQuestions();
        }

        public string ParticipantName { get; set; }
        public string ParticipantID { get; set; }
        public double Weight { get; set; } // == Question D21 (entered via keyboard not slider)
        public DateTime SurveyDate { get; set; }
        public string SurveyGUID { get; set; }
        public string SurveyYYYYMMDD => SurveyDate.ToString("yyyyMMdd");
        public SurveySection CurrentSection { get; set; }

        public Dictionary<string, string> AsDictionary() 
        {
            var dict = new Dictionary<string, string>{
                {"study", "uqmndv1"},
                {"name", ParticipantName},
                {"user_id", ParticipantID},
                {"submission_date", SurveyDate.ToString()},
                {"submission", SurveyYYYYMMDD}
            };
            foreach (var s in _sections)
            {
                dict.Add(s.SectionVariable, s.CompletionText);
            }
            dict.Add("D21", Weight.ToString());
            foreach (var q in _questions)
            {
                dict.Add(q.QuestionVariable, Math.Round(q.AnswerValue,1).ToString());
            }
                     
            return dict;
        }
        public List<string> AsCSV()
        {
            var surveyresults = AsDictionary();
            var csv = new List<string>();
            csv.Add("Participant, SurveyID, Variable, Value");
            foreach (var response in surveyresults)
            {
                csv.Add($"{ParticipantID}, {SurveyYYYYMMDD}, {response.Key}, {response.Value}");
            }
            return csv;
        }
        public string AsEmail()
        {
            var surveyresults = AsCSV();
            var email = string.Empty;
            foreach (var line in surveyresults) email += line + "\n";
            return email;
        }

        public List<SurveySection> _sections;
        public List<SurveySection> NewSections()
        {
            if (_sections == null)
            {
                _sections = new List<SurveySection>();
                _sections.Add(new SurveySection { SectionVariable = "SN1", SectionName = "Wellbeing", SectionInfo = "These questions are about your general wellbeing.\nPlease rate the following:" });
                _sections.Add(new SurveySection { SectionVariable = "SN2", SectionName = "Mental Health", SectionInfo = "These questions are about your mental health.\nPlease rate the following:" });
                _sections.Add(new SurveySection { SectionVariable = "SN3", SectionName = "Saliva", SectionInfo = "These questions are about your saliva and ability to swallow.\nPlease rate the following:" });
                _sections.Add(new SurveySection { SectionVariable = "SN4", SectionName = "Breathing", SectionInfo = "This question is about your breathing.\nPlease rate the following:" });
                _sections.Add(new SurveySection { SectionVariable = "SN5", SectionName = "Digestion", SectionInfo = "These questions are about your appetite and digestive health.\nPlease rate the following:" });
                _sections.Add(new SurveySection { SectionVariable = "SN6", SectionName = "Left arm", SectionInfo = "These questions are about your LEFT ARM.\nPlease rate the following:" });
                _sections.Add(new SurveySection { SectionVariable = "SN7", SectionName = "Right arm", SectionInfo = "These questions are about your RIGHT ARM.\nPlease rate the following:" });
                _sections.Add(new SurveySection { SectionVariable = "SN8", SectionName = "Left leg", SectionInfo = "These questions are about your LEFT LEG.\nPlease rate the following:" });
                _sections.Add(new SurveySection { SectionVariable = "SN9", SectionName = "Right leg", SectionInfo = "These questions are about your RIGHT LEG.\nPlease rate the following:" });
                _sections.Add(new SurveySection { SectionVariable = "SN10", SectionName = "Torso", SectionInfo = "These questions are about your TORSO.\nPlease rate the following:" });
            }
            return _sections;
        }


        public List<SurveyQuestion> _questions;
        public List<SurveyQuestion> NewQuestions()
        {
            if (_questions == null)
            {
                _questions = new List<SurveyQuestion>();

                _questions.Add(new SurveyQuestion { QuestionVariable = "D15", Question = "Today I feel", MinValue = 0, MaxValue = 100, Scale = 20, DefaultValue = 50, Section = "Wellbeing", MinText = "Bad", MaxText = "Good" });
                _questions.Add(new SurveyQuestion { QuestionVariable = "D16", Question = "Compared to this time two weeks ago, I feel", MinValue = 0, MaxValue = 100, Scale = 20, DefaultValue = 50, Section = "Wellbeing", MinText = "Bad", MaxText = "Good" });
                _questions.Add(new SurveyQuestion { QuestionVariable = "D20c", Question = "I am sleeping", MinValue = 0, MaxValue = 100, Scale = 20, DefaultValue = 50, Section = "Wellbeing", MinText = "Poorly", MaxText = "Well" });
                // _questions.Add(new SurveyQuestion { QuestionVariable = "D21", Question = "Today I weigh", MinValue = 0, MaxValue = 100, Scale = 20, DefaultValue = 74, Section = "Wellbeing", MinText = "", MaxText = "kg" });

                _questions.Add(new SurveyQuestion { QuestionVariable = "X1", Question = "Depression", MinValue = 0, MaxValue = 100, Scale = 20, DefaultValue = 50, Section = "Mental Health", MinText = "Low", MaxText = "High" });
                _questions.Add(new SurveyQuestion { QuestionVariable = "X2", Question = "Anxiety", MinValue = 0, MaxValue = 100, Scale = 20, DefaultValue = 50, Section = "Mental Health", MinText = "Low", MaxText = "High" });
                _questions.Add(new SurveyQuestion { QuestionVariable = "X3", Question = "Enthusiasm", MinValue = 0, MaxValue = 100, Scale = 20, DefaultValue = 50, Section = "Mental Health", MinText = "Low", MaxText = "High" });
                _questions.Add(new SurveyQuestion { QuestionVariable = "X4", Question = "Mental Fatigue", MinValue = 0, MaxValue = 100, Scale = 20, DefaultValue = 50, Section = "Mental Health", MinText = "Low", MaxText = "High" });

                _questions.Add(new SurveyQuestion { QuestionVariable = "D20a", Question = "Saliva", MinValue = 0, MaxValue = 100, Scale = 20, DefaultValue = 50, Section = "Saliva", MinText = "Dry", MaxText = "Excess" });
                _questions.Add(new SurveyQuestion { QuestionVariable = "X5", Question = "Food Variety", MinValue = 0, MaxValue = 100, Scale = 20, DefaultValue = 50, Section = "Saliva", MinText = "Narrow", MaxText = "Wide" });
                _questions.Add(new SurveyQuestion { QuestionVariable = "X6", Question = "Food Texture", MinValue = 0, MaxValue = 100, Scale = 20, DefaultValue = 50, Section = "Saliva", MinText = "Pureed", MaxText = "Normal" });
                _questions.Add(new SurveyQuestion { QuestionVariable = "X7", Question = "Amount Consumed", MinValue = 0, MaxValue = 100, Scale = 20, DefaultValue = 50, Section = "Saliva", MinText = "A little", MaxText = "A lot" });
                _questions.Add(new SurveyQuestion { QuestionVariable = "X8", Question = "Supplements", MinValue = 0, MaxValue = 100, Scale = 20, DefaultValue = 50, Section = "Saliva", MinText = "None", MaxText = "A lot" });

                _questions.Add(new SurveyQuestion { QuestionVariable = "X9", Question = "Overall Breathing", MinValue = 0, MaxValue = 100, Scale = 20, DefaultValue = 50, Section = "Breathing", MinText = "Poor", MaxText = "Good" });
                _questions.Add(new SurveyQuestion { QuestionVariable = "X10", Question = "Shortness of breath", MinValue = 0, MaxValue = 100, Scale = 20, DefaultValue = 50, Section = "Breathing", MinText = "Constant", MaxText = "Never" });
                _questions.Add(new SurveyQuestion { QuestionVariable = "X11", Question = "Breathing while lying flat", MinValue = 0, MaxValue = 100, Scale = 20, DefaultValue = 50, Section = "Breathing", MinText = "Unable", MaxText = "Not Affected" });

                _questions.Add(new SurveyQuestion { QuestionVariable = "X12", Question = "Appetite", MinValue = 0, MaxValue = 100, Scale = 20, DefaultValue = 50, Section = "Digestion", MinText = "Poor", MaxText = "Good" });
                _questions.Add(new SurveyQuestion { QuestionVariable = "X13", Question = "Nausea", MinValue = 0, MaxValue = 100, Scale = 20, DefaultValue = 50, Section = "Digestion", MinText = "Low", MaxText = "High" });
                _questions.Add(new SurveyQuestion { QuestionVariable = "D20b", Question = "Bloating / Gas", MinValue = 0, MaxValue = 100, Scale = 20, DefaultValue = 50, Section = "Digestion", MinText = "Low", MaxText = "High" });
                _questions.Add(new SurveyQuestion { QuestionVariable = "X14", Question = "Constipation", MinValue = 0, MaxValue = 100, Scale = 20, DefaultValue = 50, Section = "Digestion", MinText = "Never", MaxText = "Often" });

                _questions.Add(new SurveyQuestion { QuestionVariable = "D19a.LA", Question = "Twitching", MinValue = 0, MaxValue = 100, Scale = 20, DefaultValue = 50, Section = "Left arm", MinText = "None", MaxText = "Severe" });
                _questions.Add(new SurveyQuestion { QuestionVariable = "D19b.LA", Question = "Cramping", MinValue = 0, MaxValue = 100, Scale = 20, DefaultValue = 50, Section = "Left arm", MinText = "None", MaxText = "Severe" });
                _questions.Add(new SurveyQuestion { QuestionVariable = "D19d.LA", Question = "Weakness", MinValue = 0, MaxValue = 100, Scale = 20, DefaultValue = 50, Section = "Left arm", MinText = "None", MaxText = "Severe" });
                _questions.Add(new SurveyQuestion { QuestionVariable = "D19c.LA", Question = "Stiffness", MinValue = 0, MaxValue = 100, Scale = 20, DefaultValue = 50, Section = "Left arm", MinText = "None", MaxText = "Severe" });
                _questions.Add(new SurveyQuestion { QuestionVariable = "D19e.LA", Question = "Pain", MinValue = 0, MaxValue = 100, Scale = 20, DefaultValue = 50, Section = "Left arm", MinText = "None", MaxText = "Severe" });
                _questions.Add(new SurveyQuestion { QuestionVariable = "D19f.LA", Question = "Physical Fatigue", MinValue = 0, MaxValue = 100, Scale = 20, DefaultValue = 50, Section = "Left arm", MinText = "None", MaxText = "Severe" });
                _questions.Add(new SurveyQuestion { QuestionVariable = "D19g.LA", Question = "Wasting", MinValue = 0, MaxValue = 100, Scale = 20, DefaultValue = 50, Section = "Left arm", MinText = "None", MaxText = "Severe" });

                _questions.Add(new SurveyQuestion { QuestionVariable = "D19a.RA", Question = "Twitching", MinValue = 0, MaxValue = 100, Scale = 20, DefaultValue = 50, Section = "Right arm", MinText = "None", MaxText = "Severe" });
                _questions.Add(new SurveyQuestion { QuestionVariable = "D19b.RA", Question = "Cramping", MinValue = 0, MaxValue = 100, Scale = 20, DefaultValue = 50, Section = "Right arm", MinText = "None", MaxText = "Severe" });
                _questions.Add(new SurveyQuestion { QuestionVariable = "D19c.RA", Question = "Weakness", MinValue = 0, MaxValue = 100, Scale = 20, DefaultValue = 50, Section = "Right arm", MinText = "None", MaxText = "Severe" });
                _questions.Add(new SurveyQuestion { QuestionVariable = "D19d.RA", Question = "Stiffness", MinValue = 0, MaxValue = 100, Scale = 20, DefaultValue = 50, Section = "Right arm", MinText = "None", MaxText = "Severe" });
                _questions.Add(new SurveyQuestion { QuestionVariable = "D19e.RA", Question = "Pain", MinValue = 0, MaxValue = 100, Scale = 20, DefaultValue = 50, Section = "Right arm", MinText = "None", MaxText = "Severe" });
                _questions.Add(new SurveyQuestion { QuestionVariable = "D19f.RA", Question = "Physical Fatigue", MinValue = 0, MaxValue = 100, Scale = 20, DefaultValue = 50, Section = "Right arm", MinText = "None", MaxText = "Severe" });
                _questions.Add(new SurveyQuestion { QuestionVariable = "D19g.RA", Question = "Wasting", MinValue = 0, MaxValue = 100, Scale = 20, DefaultValue = 50, Section = "Right arm", MinText = "None", MaxText = "Severe" });

                _questions.Add(new SurveyQuestion { QuestionVariable = "D19a.TO", Question = "Twitching", MinValue = 0, MaxValue = 100, Scale = 20, DefaultValue = 50, Section = "Torso", MinText = "None", MaxText = "Severe" });
                _questions.Add(new SurveyQuestion { QuestionVariable = "D19b.TO", Question = "Cramping", MinValue = 0, MaxValue = 100, Scale = 20, DefaultValue = 50, Section = "Torso", MinText = "None", MaxText = "Severe" });
                _questions.Add(new SurveyQuestion { QuestionVariable = "D19c.TO", Question = "Weakness", MinValue = 0, MaxValue = 100, Scale = 20, DefaultValue = 50, Section = "Torso", MinText = "None", MaxText = "Severe" });
                _questions.Add(new SurveyQuestion { QuestionVariable = "D19d.TO", Question = "Stiffness", MinValue = 0, MaxValue = 100, Scale = 20, DefaultValue = 50, Section = "Torso", MinText = "None", MaxText = "Severe" });
                _questions.Add(new SurveyQuestion { QuestionVariable = "D19e.TO", Question = "Pain", MinValue = 0, MaxValue = 100, Scale = 20, DefaultValue = 50, Section = "Torso", MinText = "None", MaxText = "Severe" });
                _questions.Add(new SurveyQuestion { QuestionVariable = "D19f.TO", Question = "Physical Fatigue", MinValue = 0, MaxValue = 100, Scale = 20, DefaultValue = 50, Section = "Torso", MinText = "None", MaxText = "Severe" });
                _questions.Add(new SurveyQuestion { QuestionVariable = "D19g.TO", Question = "Wasting", MinValue = 0, MaxValue = 100, Scale = 20, DefaultValue = 50, Section = "Torso", MinText = "None", MaxText = "Severe" });

                _questions.Add(new SurveyQuestion { QuestionVariable = "D19a.LL", Question = "Twitching", MinValue = 0, MaxValue = 100, Scale = 20, DefaultValue = 50, Section = "Left leg", MinText = "None", MaxText = "Severe" });
                _questions.Add(new SurveyQuestion { QuestionVariable = "D19b.LL", Question = "Cramping", MinValue = 0, MaxValue = 100, Scale = 20, DefaultValue = 50, Section = "Left leg", MinText = "None", MaxText = "Severe" });
                _questions.Add(new SurveyQuestion { QuestionVariable = "D19d.LL", Question = "Weakness", MinValue = 0, MaxValue = 100, Scale = 20, DefaultValue = 50, Section = "Left leg", MinText = "None", MaxText = "Severe" });
                _questions.Add(new SurveyQuestion { QuestionVariable = "D19c.LL", Question = "Stiffness", MinValue = 0, MaxValue = 100, Scale = 20, DefaultValue = 50, Section = "Left leg", MinText = "None", MaxText = "Severe" });
                _questions.Add(new SurveyQuestion { QuestionVariable = "D19e.LL", Question = "Pain", MinValue = 0, MaxValue = 100, Scale = 20, DefaultValue = 50, Section = "Left leg", MinText = "None", MaxText = "Severe" });
                _questions.Add(new SurveyQuestion { QuestionVariable = "D19f.LL", Question = "Physical Fatigue", MinValue = 0, MaxValue = 100, Scale = 20, DefaultValue = 50, Section = "Left leg", MinText = "None", MaxText = "Severe" });
                _questions.Add(new SurveyQuestion { QuestionVariable = "D19g.LL", Question = "Wasting", MinValue = 0, MaxValue = 100, Scale = 20, DefaultValue = 50, Section = "Left leg", MinText = "None", MaxText = "Severe" });

                _questions.Add(new SurveyQuestion { QuestionVariable = "D19a.RL", Question = "Twitching", MinValue = 0, MaxValue = 100, Scale = 20, DefaultValue = 50, Section = "Right leg", MinText = "None", MaxText = "Severe" });
                _questions.Add(new SurveyQuestion { QuestionVariable = "D19b.RL", Question = "Cramping", MinValue = 0, MaxValue = 100, Scale = 20, DefaultValue = 50, Section = "Right leg", MinText = "None", MaxText = "Severe" });
                _questions.Add(new SurveyQuestion { QuestionVariable = "D19c.RL", Question = "Weakness", MinValue = 0, MaxValue = 100, Scale = 20, DefaultValue = 50, Section = "Right leg", MinText = "None", MaxText = "Severe" });
                _questions.Add(new SurveyQuestion { QuestionVariable = "D19d.RL", Question = "Stiffness", MinValue = 0, MaxValue = 100, Scale = 20, DefaultValue = 50, Section = "Right leg", MinText = "None", MaxText = "Severe" });
                _questions.Add(new SurveyQuestion { QuestionVariable = "D19e.RL", Question = "Pain", MinValue = 0, MaxValue = 100, Scale = 20, DefaultValue = 50, Section = "Right leg", MinText = "None", MaxText = "Severe" });
                _questions.Add(new SurveyQuestion { QuestionVariable = "D19f.RL", Question = "Physical Fatigue", MinValue = 0, MaxValue = 100, Scale = 20, DefaultValue = 50, Section = "Right leg", MinText = "None", MaxText = "Severe" });
                _questions.Add(new SurveyQuestion { QuestionVariable = "D19g.RL", Question = "Wasting", MinValue = 0, MaxValue = 100, Scale = 20, DefaultValue = 50, Section = "Right leg", MinText = "None", MaxText = "Severe" });

            }
            return _questions;
        }

    }
}
