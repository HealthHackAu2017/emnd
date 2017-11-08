from rest_framework import serializers
from medical_form.models import Submission


class SubmissionSerializer(serializers.ModelSerializer):

    class Meta:
        model = Submission
        fields = ('id', 'submission_date','user_id',
            'd20a', 'd20b', 'd20c', 'd20d', #HEAD
            'd21a', 'd21b', 'd21c', 'd21d', 'd21e', 'd21f', #THROAT
            'd22a', 'd22b', 'd22c' #LUNGS
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
        instance.d20a = validated_data.get('d20a', instance.D20a)
        instance.d20b = validated_data.get('d20b', instance.D20b)
        instance.d20c = validated_data.get('d20c', instance.D20c)
        instance.d20d = validated_data.get('d20d', instance.d20d)

        # D21 THROAT
        instance.d21a = validated_data.get('d21a', instance.d21a)
        instance.d21b = validated_data.get('d21b', instance.d21b)
        instance.d21c = validated_data.get('d21c', instance.d21c)
        instance.d21d = validated_data.get('d21d', instance.d21d)
        instance.d21e = validated_data.get('d21e', instance.d21e)
        instance.d21f = validated_data.get('d21f', instance.d21f)

        # D22 LUNGS
        instance.d22a = validated_data.get('d22a', instance.d22a)
        instance.d22b = validated_data.get('d22b', instance.d22b)
        instance.d22c = validated_data.get('d22c', instance.d22c)


        # instance.q1 = validated_data.get('q1', instance.q1)
        # instance.q2 = validated_data.get('q2', instance.q1)
        # instance.q3 = validated_data.get('q3', instance.q1)



        # instance.linenos = validated_data.get('linenos', instance.linenos)
        # instance.language = validated_data.get('language', instance.language)
        # instance.style = validated_data.get('style', instance.style)
        instance.save()
        return instance