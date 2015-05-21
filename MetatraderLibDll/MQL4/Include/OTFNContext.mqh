//+------------------------------------------------------------------+
//|                                                OTFNFunctions.mqh |
//|                        Copyright 2015, MetaQuotes Software Corp. |
//|                                             https://www.mql5.com |
//+------------------------------------------------------------------+
#property copyright "Copyright 2015, MetaQuotes Software Corp."
#property link      "https://www.mql5.com"
#property strict

#define CMD_PING            0
#define CMD_ORDERS          1

struct OrderInfo
{
    int ticket;
    int type;
    double lots;
    double openPrice;
    double closePrice;
    double takeProfit;
    double stopLoss;
    long expiration;
    long openTime;
    long closeTime;
    int magic;
};

#import "OTFNBridge.dll"
    int OTFN_Init(string brokerName, string accountId, string strategyName, int magic, string symbol, int timeframe);
    int OTFN_SendOrders(int expertId, OrderInfo &orders[], int ordersCount);
#import



class OTFNContext
{
public:
    OTFNContext(string strategyName, int magic)
    {
        this.expertId = magic;
        OTFN_Init(AccountCompany(), (string)AccountNumber(), strategyName, expertId, Symbol(), Period());
    }
    
    void DoOnTick()
    {
        MqlTick last_tick;
        if(SymbolInfoTick(Symbol(),last_tick))
        {
            OTFN_SendTick(expertId, last_tick);
        }
        
        UpdateOrders();
        
        ProcessRequests();
    }
    
    void DoOnTimer()
    {
        ProcessRequests();
    }
    
    void DoOnDeinit()
    {
    }
    
    double DoOnTester()
    {
        return 0;
    }
    
    void ProcessRequests()
    {
        double doubles[16];
        string strings[16];
        int rv = 0;
        do
        {
            rv = OTFN_GetPendingRequest(doubles, strings);
            if(rv <= 0)
                break;
            switch(rv)
            {
                case CMD_ORDERS:
                    CmdOrders();
                    break;
            }
        } while(true);
    }

private:
    OrderInfo activeOrders[];
    int expertId;
    
    
    void CmdOrders()
    {
        OTFN_SendOrders(expertId, activeOrders, ArraySize(activeOrders));
    }
    

    ////////////////////////////////////////////
    
    int UpdateOrders()
    {
        int total = OrdersTotal();
        OrderInfo orders[];
        ArrayResize(orders, total);
        
        for(int i = 0; i < total; i++)
        {
            if(!OrderSelect(i, SELECT_BY_POS, MODE_TRADES))
            {
                OrderInfo info = {};
                info.ticket = OrderTicket();
                info.type = OrderType();
                info.lots = OrderLots();
                info.magic = OrderMagicNumber();
                info.openPrice = OrderOpenPrice();
                info.closePrice = OrderClosePrice();
                info.openTime = OrderOpenTime();
                info.closeTime = OrderCloseTime();
                info.stopLoss = OrderStopLoss();
                info.takeProfit = OrderTakeProfit();
            }
            else return UpdateOrders();
        }
        
        // TODO: Detect changes
        
        ArrayCopy(activeOrders, orders);
        return 0;
    }
};



///////////////////////////////////////////////////
// Internals

void OTFN_SendTick(int expertId, MqlTick &tick)
{
}

int OTFN_GetPendingRequest(double &doubles[], string &strings[])
{
    return 0;
}
