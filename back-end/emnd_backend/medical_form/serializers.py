from rest_framework import serializers
from medical_form.models import Submission


class SubmissionSerializer(serializers.ModelSerializer):

    class Meta:
        model = Submission
        fields = ('id', 'submission_date','user_id',
            'D20a', 'D20b', 'D20c', 'D20d', #HEAD
            'D21a', 'D21b', 'D21c', 'D21d', 'D21e', 'D21f' #THROAT
            'D22a', 'D22b', 'D22c' #LUNGS
         )

    #linenos = serializers.BooleanField(required=False)
    #language = serializers.ChoiceField(choices=LANGUAGE_CHOICES, default='python')
    #style = serializers.ChoiceField(choices=STYLE_CHOICES, default='friendly')

    def create(self, validated_data):
        """
        Create and return a new `Snippet` instance, given the validated data.
        """
        return Submission.objects.create(**validated_data)

    def update(self, instance, validated_data):
        """
        Update and return an existing `submission` instance, given the validated data.
        """
        instance.user_id = validated_data.get('user_id', instance.user_id)
        

        # D20 HEAD
        instance.D20a = validated_data.get('D20a', instance.D20a)
        instance.D20b = validated_data.get('D20b', instance.D20b)
        instance.D20c = validated_data.get('D20c', instance.D20c)
        instance.D20d = validated_data.get('D20d', instance.D20d)
       
        # D21 THROAT
        instance.D21a = validated_data.get('D21a', instance.D21a)
        instance.D21b = validated_data.get('D21b', instance.D21b)
        instance.D21c = validated_data.get('D21c', instance.D21c)
        instance.D21d = validated_data.get('D21d', instance.D21d)
        instance.D21e = validated_data.get('D21e', instance.D21e)
        instance.D21e = validated_data.get('D21f', instance.D21f)

        # D22 LUNGS
        instance.D22a = validated_data.get('D22a', instance.D22a)
        instance.D22b = validated_data.get('D22b', instance.D22b)
        instance.D22c = validated_data.get('D22c', instance.D22c)


        # instance.q1 = validated_data.get('q1', instance.q1)
        # instance.q2 = validated_data.get('q2', instance.q1)
        # instance.q3 = validated_data.get('q3', instance.q1)



        # instance.linenos = validated_data.get('linenos', instance.linenos)
        # instance.language = validated_data.get('language', instance.language)
        # instance.style = validated_data.get('style', instance.style)
        instance.save()
        return instance