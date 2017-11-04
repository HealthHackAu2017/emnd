from rest_framework import serializers
from medical_form.models import Submission


class SubmissionSerializer(serializers.ModelSerializer):

    class Meta:
        model = Submission
        fields = ('id', 'user_id', 'q1', 'q2', 'q3')

    #linenos = serializers.BooleanField(required=False)
    #language = serializers.ChoiceField(choices=LANGUAGE_CHOICES, default='python')
    #style = serializers.ChoiceField(choices=STYLE_CHOICES, default='friendly')

    def create(self, validated_data):
        """
        Create and return a new `Snippet` instance, given the validated data.
        """
        return medical_form.objects.create(**validated_data)

    def update(self, instance, validated_data):
        """
        Update and return an existing `Snippet` instance, given the validated data.
        """
        instance.user_id = validated_data.get('user_id', instance.user_id)
        instance.q1 = validated_data.get('q1', instance.q1)
        instance.q2 = validated_data.get('q2', instance.q1)
        instance.q3 = validated_data.get('q3', instance.q1)

       # instance.linenos = validated_data.get('linenos', instance.linenos)
       # instance.language = validated_data.get('language', instance.language)
       # instance.style = validated_data.get('style', instance.style)
        instance.save()
        return instance