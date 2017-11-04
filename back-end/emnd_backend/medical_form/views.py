from django.http import HttpResponse, JsonResponse
from django.views.decorators.csrf import csrf_exempt
from rest_framework.renderers import JSONRenderer
from rest_framework.parsers import JSONParser
from medical_form.models import Submission
from medical_form.serializers import SubmissionSerializer




@csrf_exempt
def submission_list(request):
    if request.method == 'GET':
        submissions = Submission.objects.all()
        serializer = SubmissionSerializer(submissions, many=True)
        return JsonResponse(serializer.data, safe=False)

    elif request.method == 'POST':
        data = JSONParser().parse(request)
        serializer = SubmissionSerializer(data=data)
        if serializer.is_valid():
            serializer.save()
            return JsonResponse(serializer.data, status=201)
        return JsonResponse(serializer.errors, status=400)


@csrf_exempt
def submission_detail(request, pk):
    """
    Retrieve, update or delete a code submission.
    """
    try:
        submission = Submission.objects.get(pk=pk)
    except Submission.DoesNotExist:
        return HttpResponse(status=404)

    if request.method == 'GET':
        serializer = SubmissionSerializer(submission)
        return JsonResponse(serializer.data)

    elif request.method == 'PUT':
        data = JSONParser().parse(request)
        serializer = SubmissionSerializer(submission, data=data)
        if serializer.is_valid():
            serializer.save()
            return JsonResponse(serializer.data)
        return JsonResponse(serializer.errors, status=400)

    elif request.method == 'DELETE':
        submission.delete()
        return HttpResponse(status=204)
