from django.conf.urls import url
from medical_form import views

urlpatterns = [
    url(r'^submissions/$', views.submission_list),
    url(r'^submissions/submit/$', views.submission_post),
    url(r'^submissions/(?P<pk>[0-9]+)/$', views.submission_detail),
]