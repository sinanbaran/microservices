FROM python
RUN mkdir /usr/src/app/
COPY . /usr/src/app/
WORKDIR /usr/src/app/
EXPOSE 5000
RUN pip install flask
RUN pip install Flask-PyMongo
CMD ["python", "app.py"]