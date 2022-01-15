# GuestBook: Documentation

This app is made as a hiring task for Coformatique 2022.

## Abstract

This is a website where a user can make an event and recieve messages from friends and reply to these messages.

## 1. Project Scope

### a. Background:

A virtual guestbook is a guestbook specifically for an online wedding celebration, which allows guests to share well wishes, photos, and videos that are then are compiled into a digital memory of the day. Especially nowadays, where COVID-19 could spread and infect a lot of people. In our applications, we want to make it easy for users to make online events to limit the spread of diseases.

### b. Stakeholders/Beneficiaries:
- future spouses.
- Distance communicators.

<hr />

## 2. Requirements

### a. Functional Requirements: (Features)

#### 1. Sending messages:
- Friends can send messages to the event owner.
- Also, can edit and delete messages.

#### 2. Replying to messages:
- User can reply to, edit and delete messages.

### b. List of Use Cases:

##### The users can:
- Sign up.
- Sign in (can stay logged in).
- View received messages.
- Send code to friends to send messages to him.
- Reply to their messages.
- Edit, delete and show messages details.

<hr />

## 3. Design Overflow

#### a. System Architecture:

<p align="center">
<img src="https://upload.wikimedia.org/wikipedia/commons/5/53/Router-MVC-DB.svg" width="500">
 </p>
 
# How to Use

### a. To receive messages:
- At home page you will find a code.
- Copy this code and send it to a friend.
- Wait for friends to send their messages.

### b. To send a message:
- Use the code that your friend send you.
- Click the (Send a message) button at the navbar.
- Paste your code and write your message, then click send.

### c. To Reply to a message:
- Under Each message you will find a (reply) button.
- Click reply you will redirected to another page to write your reply.

## 4. Future Work
 - Enable the user to create different events.
 - Allow the user to add images for the event.
 - Market for the app through social media networks.
<hr />

## Code Improvements To-Do List.
- Add more unit test classes.
- Use Microsoft Identity Authentication.

<hr />

## Design Decisions
- Used ASP.Net Core [.NET 6] because it's fast and reliable.
- Used MVC Architechure pattern because it provieds more organization to the program and faster development proccess.
