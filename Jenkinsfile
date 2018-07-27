#!/bin/groovy

node ('xamarin-macios') {
    stage ("Build") {
        def attachments = """
[
        {
            \"text\": \"And here’s an attachment!\"
        }
]"""
        echo (attachments)
        slackSend (channel: "@rolf", color: "danger", message: "Test message 2", attachments: attachments)
    }
} // node
