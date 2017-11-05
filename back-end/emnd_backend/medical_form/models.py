from django.db import models
from pygments.lexers import get_all_lexers
from pygments.styles import get_all_styles
from users.models import User

#LEXERS = [item for item in get_all_lexers() if item[1]]
#LANGUAGE_CHOICES = sorted([(item[1][0], item[0]) for item in LEXERS])
#STYLE_CHOICES = sorted((item, item) for item in get_all_styles())


class Submission(models.Model):
    submission_date = models.DateTimeField(auto_now_add=True)
    user_id = models.IntegerField()
    
    #HEAD
    D20a = models.IntegerField(default=0)
    D20b = models.IntegerField(default=0)
    D20c = models.IntegerField(default=0)
    D20d = models.IntegerField(default=0)

    #  THROAT 
    D21a = models.IntegerField(default=0)    
    D21b = models.IntegerField(default=0)
    D21c = models.IntegerField(default=0)
    D21d = models.IntegerField(default=0)
    D21e = models.IntegerField(default=0)
    D21f = models.IntegerField(default=0)

    # LUNGS
    D22a = models.IntegerField(default=0)
    D22b = models.IntegerField(default=0)
    D22c = models.IntegerField(default=0)


    
#    class Meta:
#       ordering = ('created',)
