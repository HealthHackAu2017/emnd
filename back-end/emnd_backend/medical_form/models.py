from django.db import models
from pygments.lexers import get_all_lexers
from pygments.styles import get_all_styles
from users.models import User

#LEXERS = [item for item in get_all_lexers() if item[1]]
#LANGUAGE_CHOICES = sorted([(item[1][0], item[0]) for item in LEXERS])
#STYLE_CHOICES = sorted((item, item) for item in get_all_styles())


class Submission(models.Model):
    submission_date = models.DateTimeField(auto_now_add=True)
    user_id = models.ForeignKey(User, on_delete=models.CASCADE)
    
    # HEAD
    d20a = models.IntegerField(default=0)
    d20b = models.IntegerField(default=0)
    d20c = models.IntegerField(default=0)
    d20d = models.IntegerField(default=0)

    # THROAT 
    d21a = models.IntegerField(default=0)    
    d21b = models.IntegerField(default=0)
    d21c = models.IntegerField(default=0)
    d21d = models.IntegerField(default=0)
    d21e = models.IntegerField(default=0)
    d21f = models.IntegerField(default=0)

    # LUNGS
    d22a = models.IntegerField(default=0)
    d22b = models.IntegerField(default=0)
    d22c = models.IntegerField(default=0)


    
    class Meta:
        ordering = ('submission_date',)
