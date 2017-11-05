using System;
using System.Collections.Generic;

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
            public string SectionName { get; set; }
            public string SectionInfo { get; set; }
            public bool SectionSkipped { get; set; }
            public bool SectionCompleted { get; set; }

        public List<SurveyQuestion> SectionQuestions { get; set; }
        //{
        //    get
        //    {
        //        return new List<SurveyQuestion>();
        //    }
        //}
            
        }

    public partial class Survey
    {
        public DateTime SurveyDate { get; set; }
        public string SurveyYYYYMMDD { get; set; }
        public string ParticipantName { get; set; }
        public string ParticipantID { get; set; }
        public bool EnteredByParticipant { get; set; }
        public string EnteredBy { get; set; }
        public double Weight { get; set; }
        public SurveySection CurrentSection { get; set; }

        public List<SurveySection> _sections;
        public List<SurveySection> NewSections()
        {
            if (_sections == null)
            {
                _sections = new List<SurveySection>();
                _sections.Add(new SurveySection { SectionName = "Wellbeing", SectionInfo = "These questions are about your general wellbeing.\nPlease rate the following:" });
                _sections.Add(new SurveySection { SectionName = "Mental Health", SectionInfo = "These questions are about your mental health.\nPlease rate the following:" });
                _sections.Add(new SurveySection { SectionName = "Saliva", SectionInfo = "These questions are about your saliva and ability to swallow.\nPlease rate the following:" });
                _sections.Add(new SurveySection { SectionName = "Breathing", SectionInfo = "This question is about your breathing.\nPlease rate the following:" });
                _sections.Add(new SurveySection { SectionName = "Digestion", SectionInfo = "These questions are about your appetite and digestive health.\nPlease rate the following:" });
                _sections.Add(new SurveySection { SectionName = "Left arm", SectionInfo = "These questions are about your LEFT ARM.\nPlease rate the following:" });
                _sections.Add(new SurveySection { SectionName = "Right arm", SectionInfo = "These questions are about your RIGHT ARM.\nPlease rate the following:" });
                _sections.Add(new SurveySection { SectionName = "Left leg", SectionInfo = "These questions are about your LEFT LEG.\nPlease rate the following:" });
                _sections.Add(new SurveySection { SectionName = "Right leg", SectionInfo = "These questions are about your RIGHT LEG.\nPlease rate the following:" });
                _sections.Add(new SurveySection { SectionName = "Torso", SectionInfo = "These questions are about your TORSO.\nPlease rate the following:" });

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

                _questions.Add(new SurveyQuestion { QuestionVariable = "D15", Question = "Depression", MinValue = 0, MaxValue = 100, Scale = 20, DefaultValue = 50, Section = "Mental Health", MinText = "Low", MaxText = "High" });
                _questions.Add(new SurveyQuestion { QuestionVariable = "D15", Question = "Anxiety", MinValue = 0, MaxValue = 100, Scale = 20, DefaultValue = 50, Section = "Mental Health", MinText = "Low", MaxText = "High" });
                _questions.Add(new SurveyQuestion { QuestionVariable = "D15", Question = "Enthusiasm", MinValue = 0, MaxValue = 100, Scale = 20, DefaultValue = 50, Section = "Mental Health", MinText = "Low", MaxText = "High" });
                _questions.Add(new SurveyQuestion { QuestionVariable = "D15", Question = "Mental Fatigue", MinValue = 0, MaxValue = 100, Scale = 20, DefaultValue = 50, Section = "Mental Health", MinText = "Low", MaxText = "High" });

                _questions.Add(new SurveyQuestion { QuestionVariable = "D20a", Question = "Saliva", MinValue = 0, MaxValue = 100, Scale = 20, DefaultValue = 50, Section = "Saliva", MinText = "Dry", MaxText = "Excess" });
                _questions.Add(new SurveyQuestion { QuestionVariable = "D15", Question = "Food Variety", MinValue = 0, MaxValue = 100, Scale = 20, DefaultValue = 50, Section = "Saliva", MinText = "Narrow", MaxText = "Wide" });
                _questions.Add(new SurveyQuestion { QuestionVariable = "D15", Question = "Food Texture", MinValue = 0, MaxValue = 100, Scale = 20, DefaultValue = 50, Section = "Saliva", MinText = "Pureed", MaxText = "Normal" });
                _questions.Add(new SurveyQuestion { QuestionVariable = "D15", Question = "Amount Consumed", MinValue = 0, MaxValue = 100, Scale = 20, DefaultValue = 50, Section = "Saliva", MinText = "A little", MaxText = "A lot" });
                _questions.Add(new SurveyQuestion { QuestionVariable = "D15", Question = "Supplements", MinValue = 0, MaxValue = 100, Scale = 20, DefaultValue = 50, Section = "Saliva", MinText = "None", MaxText = "A lot" });

                _questions.Add(new SurveyQuestion { QuestionVariable = "D15", Question = "Overall Breathing", MinValue = 0, MaxValue = 100, Scale = 20, DefaultValue = 50, Section = "Breathing", MinText = "Poor", MaxText = "Good" });
                _questions.Add(new SurveyQuestion { QuestionVariable = "D15", Question = "Shortness of breath", MinValue = 0, MaxValue = 100, Scale = 20, DefaultValue = 50, Section = "Breathing", MinText = "Constant", MaxText = "Never" });
                _questions.Add(new SurveyQuestion { QuestionVariable = "D15", Question = "Breathing while lying flat", MinValue = 0, MaxValue = 100, Scale = 20, DefaultValue = 50, Section = "Breathing", MinText = "Unable", MaxText = "Not Affected" });

                _questions.Add(new SurveyQuestion { QuestionVariable = "D15", Question = "Appetite", MinValue = 0, MaxValue = 100, Scale = 20, DefaultValue = 50, Section = "Digestion", MinText = "Poor", MaxText = "Good" });
                _questions.Add(new SurveyQuestion { QuestionVariable = "D15", Question = "Nausea", MinValue = 0, MaxValue = 100, Scale = 20, DefaultValue = 50, Section = "Digestion", MinText = "Low", MaxText = "High" });
                _questions.Add(new SurveyQuestion { QuestionVariable = "D20b", Question = "Bloating / Gas", MinValue = 0, MaxValue = 100, Scale = 20, DefaultValue = 50, Section = "Digestion", MinText = "Low", MaxText = "High" });
                _questions.Add(new SurveyQuestion { QuestionVariable = "D15", Question = "Constipation", MinValue = 0, MaxValue = 100, Scale = 20, DefaultValue = 50, Section = "Digestion", MinText = "Never", MaxText = "Often" });

                _questions.Add(new SurveyQuestion { QuestionVariable = "D19a.LA", Question = "Twitching", MinValue = 0, MaxValue = 100, Scale = 20, DefaultValue = 50, Section = "Left arm", MinText = "None", MaxText = "Severe" });
                _questions.Add(new SurveyQuestion { QuestionVariable = "D19b.LA", Question = "Cramping", MinValue = 0, MaxValue = 100, Scale = 20, DefaultValue = 50, Section = "Left arm", MinText = "None", MaxText = "Severe" });
                _questions.Add(new SurveyQuestion { QuestionVariable = "D19d.LA", Question = "Weakness", MinValue = 0, MaxValue = 100, Scale = 20, DefaultValue = 50, Section = "Left arm", MinText = "None", MaxText = "Severe" });
                _questions.Add(new SurveyQuestion { QuestionVariable = "D19c.LA", Question = "Stiffness", MinValue = 0, MaxValue = 100, Scale = 20, DefaultValue = 50, Section = "Left arm", MinText = "None", MaxText = "Severe" });
                _questions.Add(new SurveyQuestion { QuestionVariable = "D19e.LA", Question = "Pain", MinValue = 0, MaxValue = 100, Scale = 20, DefaultValue = 50, Section = "Left arm", MinText = "None", MaxText = "Severe" });
                _questions.Add(new SurveyQuestion { QuestionVariable = "D19f.LA", Question = "Physical Fatigue", MinValue = 0, MaxValue = 100, Scale = 20, DefaultValue = 50, Section = "Left arm", MinText = "None", MaxText = "Severe" });
                _questions.Add(new SurveyQuestion { QuestionVariable = "D19g.LA", Question = "Wasting", MinValue = 0, MaxValue = 100, Scale = 20, DefaultValue = 50, Section = "Left arm", MinText = "None", MaxText = "Severe" });

                _questions.Add(new SurveyQuestion { QuestionVariable = "D15", Question = "Twitching", MinValue = 0, MaxValue = 100, Scale = 20, DefaultValue = 50, Section = "Right arm", MinText = "None", MaxText = "Severe" });
                _questions.Add(new SurveyQuestion { QuestionVariable = "D15", Question = "Cramping", MinValue = 0, MaxValue = 100, Scale = 20, DefaultValue = 50, Section = "Right arm", MinText = "None", MaxText = "Severe" });
                _questions.Add(new SurveyQuestion { QuestionVariable = "D15", Question = "Weakness", MinValue = 0, MaxValue = 100, Scale = 20, DefaultValue = 50, Section = "Right arm", MinText = "None", MaxText = "Severe" });
                _questions.Add(new SurveyQuestion { QuestionVariable = "D15", Question = "Stiffness", MinValue = 0, MaxValue = 100, Scale = 20, DefaultValue = 50, Section = "Right arm", MinText = "None", MaxText = "Severe" });
                _questions.Add(new SurveyQuestion { QuestionVariable = "D15", Question = "Pain", MinValue = 0, MaxValue = 100, Scale = 20, DefaultValue = 50, Section = "Right arm", MinText = "None", MaxText = "Severe" });
                _questions.Add(new SurveyQuestion { QuestionVariable = "D15", Question = "Physical Fatigue", MinValue = 0, MaxValue = 100, Scale = 20, DefaultValue = 50, Section = "Right arm", MinText = "None", MaxText = "Severe" });
                _questions.Add(new SurveyQuestion { QuestionVariable = "D15", Question = "Wasting", MinValue = 0, MaxValue = 100, Scale = 20, DefaultValue = 50, Section = "Right arm", MinText = "None", MaxText = "Severe" });
            }
            return _questions;
        }

    }
}
