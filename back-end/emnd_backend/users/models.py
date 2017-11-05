from django.contrib.auth.models import AbstractBaseUser, PermissionsMixin, BaseUserManager
from django.db import models
from django.contrib.auth.models import UserManager

CREATOR_RELATIONS = (
    ('patient', 'I am filling this in myself'),
    ('family member or carer', 'A family member or carer'),
    ('research team member', 'A research team member'),
    ('other', 'someone else'),
)

class UserManager(BaseUserManager):
    use_in_migrations = True

    def _create_user(self, username, password, **extra_fields):
        """
        Create and save a user with the given username and password.
        """
        if not username:
            raise ValueError('The given username must be set')
        username = self.model.normalize_username(username)
        user = self.model(username=username, **extra_fields)
        user.set_password(password)
        user.save(using=self._db)
        return user

    def create_user(self, username, password=None, **extra_fields):
        extra_fields.setdefault('is_staff', False)
        extra_fields.setdefault('is_superuser', False)
        return self._create_user(username, password, **extra_fields)

    def create_superuser(self, username, password, **extra_fields):
        extra_fields.setdefault('is_staff', True)
        extra_fields.setdefault('is_superuser', True)

        if extra_fields.get('is_staff') is not True:
            raise ValueError('Superuser must have is_staff=True.')
        if extra_fields.get('is_superuser') is not True:
            raise ValueError('Superuser must have is_superuser=True.')

        return self._create_user(username, password, **extra_fields)


class User(AbstractBaseUser, PermissionsMixin):
    username = models.CharField(max_length=30, unique=True)
    creator_relation = models.CharField(choices=CREATOR_RELATIONS, max_length=100, null=True, blank=True)
    creator_name = models.CharField(max_length=100, null=True, blank=True)
    dob = models.DateField(null=True, blank=True)
    email = models.EmailField(null=True, blank=True)
    first_name = models.CharField(max_length=100, null=True, blank=True)
    last_name = models.CharField(max_length=100, null=True, blank=True)
    mobile = models.CharField(max_length=15, null=True, blank=True)
    start_of_symptoms = models.DateField(null=True, blank=True)
    date_of_mnd = models.DateField(null=True, blank=True)
    clinic_city = models.CharField(max_length=30, null=True, blank=True)
    clinic_state = models.CharField(max_length=30, null=True, blank=True)
    clinic_name = models.CharField(max_length=40, null=True, blank=True)
    clinic_other = models.CharField(max_length=100, null=True, blank=True)
    smoking_status = models.NullBooleanField(default=False, null=True, blank=True)
    prescribed_medications = models.NullBooleanField(default=False, null=True, blank=True)
    is_staff = models.NullBooleanField(default=False)
    smoking_status = models.NullBooleanField(default=False)
    init_symptoms_throat = models.NullBooleanField(default=False, verbose_name="Initial symptoms | Region: Throat")
    init_symptoms_chest_abd = models.NullBooleanField(default=False, verbose_name="Initial symptoms | Region: Chest / Abdomen")
    init_symptoms_upper_limbs = models.NullBooleanField(default=False, verbose_name="Initial symptoms | Region: Upper limbs")
    init_symptoms_lower_limbs = models.NullBooleanField(default=False, verbose_name="Initial symptoms | Region: Lower limbs")

    curr_symptoms_throat = models.NullBooleanField(default=False, verbose_name="Current symptoms | Region: Throat")
    curr_symptoms_chest_abd = models.NullBooleanField(default=False, verbose_name="Current symptoms | Region: Chest / Abdomen")
    curr_symptoms_upper_limbs = models.NullBooleanField(default=False, verbose_name="Current symptoms | Region: Upper limbs")
    curr_symptoms_lower_limbs = models.NullBooleanField(default=False, verbose_name="Current symptoms | Region: Lower limbs")

    USERNAME_FIELD = 'username'
    objects = UserManager()

    def __unicode__(self):
        return self.first_name + ' ' + self.last_name

    def get_short_name(self):
        return self.username