import * as Discord from 'discord.js';

export class DiscordBot {
    private client: Discord.Client;

    constructor() {
        this.client = new Discord.Client();

        this.client.on('ready', () => {
            let tag = this.client.user.tag;
            let users = this.client.users.array().length;
            let channels = this.client.channels.array().length;
            let guilds = this.client.guilds.array().length;

            console.log(`Logged in as ${tag}, with ${users} users, in ${channels} channels of ${guilds} guilds.!`);
            this.client.user.setActivity('Initialization');
        });
    }

    public async login(token: string) {
        await this.client.login(token);
        this.dumpTest();
    }

    private async dumpTest() {
        //let g = this.client.guilds.find((x) => { return x.name == 'server name' });
        //let c = g.channels.find((x) => { return x.name == "channel name" });
        let c = this.client.channels.find(x => x.id == '231393532974202881');
        if (c.type === 'text') {
            let tc = (c as Discord.TextChannel);
            console.log(tc);
        }

    }
    
    private async loadMessages(textChannel: Discord.TextChannel, timestampAfter: number, timestampBefore: number = Date.now()) {
        // let msg = await textChannel.fetchMessages({ limit: 100, before: Discord.SnowflakeUtil.generate(timestampBefore) });
        // msg = msg.filter(x => { return x.createdTimestamp > timestampAfter }); // Milliseconds timestamp
        // console.log(`Batch: ${msg.size} First: ${msg.first().createdAt} Last: ${msg.last().createdAt}`);
        // if (msg.size < 100) return msg;
        // else {
        //     let lastLoadedTimestamp = msg.last().createdTimestamp;
        //     let recursion = await loadMessages(textChannel, timestampAfter, lastLoadedTimestamp);
        //     return msg.concat(recursion);
        //}
    }

}