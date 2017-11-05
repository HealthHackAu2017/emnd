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

  patient_results['Mental Health'] = (patient_results.d20a + patient_results.d20b + 
    (10.0 - patient_results.d20c) + patient_results.d20d)/4.0

  patient_results['Saliva and Swallowing'] = (patient_results.d21a + (10.0 - patient_results.d21b) + 
    (10.0 - patient_results.d21c) + (10.0 - patient_results.d21d) + 
    patient_results.d21e + (10.0 - patient_results.d21f))/6.0

  patient_results['Breathing'] = ((10.0 - patient_results.d22a) + (10.0 - patient_results.d22b) +
    (10.0 - patient_results.d22c))/3.0

  patient_results['Overall Health'] = (patient_results['Mental Health'] + patient_results['Saliva and Swallowing'] + 
    patient_results['Breathing'])/3.0

  output = patient_results[['Mental Health','Saliva and Swallowing','Breathing','Overall Health']]

  output.to_dict()
  
  response = {}
  response['Mental Health'] = output['Mental Health'][0]
  response['Saliva and Swallowing'] = output['Saliva and Swallowing'][0]
  response['Breathing'] = output['Breathing'][0]
  response['Overall Health'] = output['Overall Health'][0]

  print(response)

  return response