const Discord = require("discord.js")
const client = new Discord.Client({ intents: 32767, allowedMentions: { parse: [] } })
const express = require("express")
const app = express()
var bodyParser = require('body-parser');
app.use(bodyParser.json());       // to support JSON-encoded bodies
app.use(bodyParser.urlencoded({     // to support URL-encoded bodies
  extended: true
}));
require("dotenv").config()
app.listen(8080)
app.get("/", (req, res) => {
  res.send('botisok')
})
app.post("/get", (req, res) => {
  console.log(req)

  const a = req.body['content'].split('|')

  getmes(a[0], a[1]).then((aa) => {
    if (aa == "error") {
      return res.send("error")
    }
    let messages = [];
    aa.forEach(message => {
      messages.push(
        {
          "content": message.content,
          "author": message.author.id,
          "name": message.author.username,
          "time": message.createdTimestamp,
          "avatar": message.author.displayAvatarURL({ format: "png", size: 1024 })
        }
      )
    })
    console.log(webhookurl(a[0],a[1]))
    return res.send({ "data": messages ,"webhook":webhookurl(a[1])})
  }

  )

})

app.get("/url", (req, res) => {
  res.redirect("discordtowindows:" + req.query.url)
})

client.once("ready", () => {
  console.log("botisready")
})

client.on("messageCreate", async message => {
  if (message.author.bot) return;

  if (message.mentions.users.has(client.user.id)) {
    const content = message.content;
    console.log(content)
    if (content.indexOf("ping") != -1) {
      console.log(message.guild.id)
      message.reply(`Pong!${client.ws.ping}ms.`)
    }
    else {
      const b = message.channel.id + "|" + message.guild.id
      const emb = {
        embeds: [{
          title: "about",
          description: "discord上の文章を表示するためにメッセージを取得するだけのbotだよ!\n下のやつをコピーしてexeのidに貼り付けてください。\n`" + b + "`"
        }]
      }
      message.reply(emb)
    }
  }
})





client.login(process.env.DISCORD_TOKEN)

function getmes(channelid, guildid) {
  try {
    const guild = client.guilds.cache.get(guildid)
    const channel = guild.channels.cache.get(channelid)
    return channel.messages.fetch({ limit: 10 })
  }
  catch (e) {
    console.log(e)
    return "error"
  }

}

function webhookurl (channelid,guildid){
  const guild = client.guilds.cache.get(guildid)
  const channel = guild.channels.cache.get(channelid)
  let url="";
  channel.fetchWebhooks.then(webhooks=>{
    if(webhooks.size==0){
      return channel.createWebhook().url
    }
    else{
     return webhooks[0].url
    }
  })
}