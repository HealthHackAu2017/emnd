from medical_form.models import Submission
import pandas as pd

def processAnalytics(userId):
  submissions = Submission.objects.filter(user_id=userId).reverse()[:2]
  # if len(submissions) > 1:
  #   print(submissions[0].__dict__)
  #   print(submissions[1].__dict__)
  # else:
  #   print(submissions[0].__dict__)

  print(submissions[0].__dict__)
  patient_results = pd.DataFrame([submissions[0].__dict__])

  patient_results['d20'] = (patient_results.d20a + patient_results.d20b + 
    (10.0 - patient_results.d20c) + patient_results.d20d)/4.0

  patient_results['d21'] = (patient_results.d21a + (10.0 - patient_results.d21b) + 
    (10.0 - patient_results.d21c) + (10.0 - patient_results.d21d) + 
    patient_results.d21e + (10.0 - patient_results.d21f))/6.0

  patient_results['d22'] = ((10.0 - patient_results.d22a) + (10.0 - patient_results.d22b) +
    (10.0 - patient_results.d22c))/3.0

  patient_results['Overall_Health'] = (patient_results.d20 + patient_results.d21 + 
    patient_results.d22)/3.0

  output = patient_results[['d20','d21','d22','Overall_Health']]

  output.to_dict()
  output['d20']['name'] = 'Mental Health'
  output['d21']['name'] = 'Saliva and Swallowing'
  output['d22']['name'] = 'Breathing'
  output['Overall_Health']['name'] = 'Overall Health'


  print(output)

  return output