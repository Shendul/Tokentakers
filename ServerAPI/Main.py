"""Hello World API implemented using Google Cloud Endpoints.

Defined here are the ProtoRPC messages needed to define Schemas for methods
as well as those methods defined in an API.
"""


import endpoints
from protorpc import messages
from protorpc import message_types
from protorpc import remote
from models import User
import logging

package = 'Hello'


class LoginRequestMessage(messages.Message):
  username = messages.StringField(1)
  password = messages.StringField(2)


class LoginResponseMessage(messages.Message):
  isSuccessful = messages.BooleanField(1)

class SignUpRequestMessage(messages.Message):
  username = messages.StringField(1)
  email = messages.StringField(2)
  password = messages.StringField(3)
  passwordAgain = messages.StringField(4)

class SignUpResponseMessage(messages.Message):
  isSuccessful = messages.BooleanField(1)


@endpoints.api(name='tokenTakersApi', version='v1')
class TokenTakersApi(remote.Service):
  """Token Takers API v1."""

  @endpoints.method(LoginRequestMessage, LoginResponseMessage,
                    path='login', http_method='POST',
                    name='tokentakers.login')
  def login(self, request):
    # Do login logic here. (TODO: Don't handle passwords as plaintext)
    query = User.query_login(request.username, request.password).fetch()
    if len(query) != 1:
      return LoginResponseMessage(isSuccessful=False)
    return LoginResponseMessage(isSuccessful=True)


  @endpoints.method(SignUpRequestMessage, SignUpResponseMessage,
                    path='signup', http_method='POST',
                    name='tokentakers.signup')
  def signup(self, request):
    # Do sign up logic here
    # Make sure all the data is there:
    result = User.signup_user(request.username,
                              request.email,
                              request.password,
                              request.passwordAgain)
    return SignUpResponseMessage(isSuccessful=result)

APPLICATION = endpoints.api_server([TokenTakersApi])
