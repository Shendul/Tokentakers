import endpoints
import logging
from google.appengine.ext import ndb

class User(ndb.Model):
  username = ndb.StringProperty()
  email = ndb.StringProperty()
  password = ndb.StringProperty()

  @classmethod
  def query_login(cls, username, password):
    return cls.query(cls.username == username, cls.password == password)

  @classmethod
  def signup_user(cls, username, email, password, passwordAgain):
    # TODO: replace return False with a helpful error message.
    # Check to see if the username or email are already in use
    if cls.query(ndb.OR(cls.username == username, cls.email == email)).fetch():
      logging.info("A user tried to sign up even though they already have an account.")
      return False
    # Check to see if the passwords match.
    if password != passwordAgain:
      logging.info("Passwords don't match on signup!")
      return False

    if len(password) < 2 or len(username) < 2 or len(email) < 2:
      logging.info("Data is too short on signup.")
      return False

    User(username=username, email=email, password=password).put()
    return True
