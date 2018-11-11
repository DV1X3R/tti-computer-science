use chatap

db.datasources.insertOne(
    {
        "_id": new ObjectId(),
        "extractor":
        {
            "type": "Chatbot",
            "platform": "Discord",
            "identifier": "MyBot#1234"
        },
        "sourceId": "guild1",
        "name": "Programming Community #1",
        "createdAt": new Date("2017-01-01T16:42:05"),
        "iconURL": "http://...",
        "channels": [
            {
                "sourceId": "channel1",
                "name": "#front-end",
                "type": "text",
                "createdAt": new Date("2017-01-01T17:02:09")
            },
            {
                "sourceId": "channel2",
                "name": "#back-end",
                "type": "text",
                "createdAt": new Date("2017-01-02T11:33:16")
            }
        ],
        "users": [
            {
                "sourceId": "user1",
                "userName": "dv1x3r#1337",
                "bot": false
            },
            {
                "sourceId": "user2",
                "userName": "Jack#4321",
                "bot": false
            }
        ]
    }
)

db.messages.insertOne(
    {
        "_id": new ObjectID(),
        "dataSourceId": ObjectId("xxxx"),
        "channelId": "channel1",
        "userId": "user1",
        "sourceId": "msg1",
        "createdAt": new Date("2017-01-02T11:33:16"),
        "cleanContent": "Hello @jack",
        "attachments": 0,
        "mentions": [
            {
                "userId": "user2",
                "count": 1
            }],
        "reactions": [
            {
                "userIds": ["user2"],
                "reaction": ":kappa:"
            }
        ]
    }
)

db.processed.insertOne(
    {
        "_id": new ObjectID(),
        "dataSourceId": ObjectId("xxxx"),
        "channelId": "channel1",
        "userId": "user1",
        "createdAtHour": new Date("2017-01-02T11:00:00"),
        "messages": 1,
        "words": 2,
        "attachments": 0,
        "reactions": [
            {
                "reaction": ":kappa:",
                "count": 1
            }],
        "perspectiveApi": [0.02]
    }
)
