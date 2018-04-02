# ProHashingWamp
Very simple console app to dump data from ProHashing wamp using wampsharp. (http://wampsharp.net/)

I've created this simple app that will dump the results of ProHashing wamp functions into json file in the folder where the app is run.

It only handles the following:
f_all_balance_updates
f_all_profitability_updates
f_all_miner_updates

Please feel free to use it for whatever purpose you need.  I personally then use golang to pick up the json and provide metrics to prometheus/grafana.  I do not suggest running this app more often than every minute.

Took some time to work out how to implement the wamp stuff, but hope this helps.

In no way do I guarantee anything.  This was put together with very little in mind in terms of security, error handling and so forth.  It does not reflect any sort of good practice.

Its been tested on windows 10 using .net 4.5

If you would like some features added, just ask... I'll see what I can do, no guarantees.

If you find this helpfull, please consider donating/tiping:

BTC: 1593CqFcBuisDs7QudsvgVmmt2iJ5Jn7Yy

ZEC: t1bQpno1U4RoJaGHCytwBqEEz16X6bGjSHe

ETH: 0x7ceae46b0d0fdcddd8f074866a188eb43497ded0

LTC: LS7rDpAB6WWySsXB2ThidK9SemQieGeMfa

DSH: XxUL9hVUAZsHTk2XnnEHABMPZqGW5FZcM6

DGE: DSGkYLzKkKPaquiDBWsciJDRak47HGoWWw

BCH: 1KHGH6Po4kGQMun8HS9sXiJsgGri4q9TQq

Special thanks to the developers of wampsharp.
