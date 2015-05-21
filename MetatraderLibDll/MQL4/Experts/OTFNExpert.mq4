//+------------------------------------------------------------------+
//|                                                   OTFNExpert.mq4 |
//|                        Copyright 2015, MetaQuotes Software Corp. |
//|                                             https://www.mql5.com |
//+------------------------------------------------------------------+
#property copyright "Copyright 2015, MetaQuotes Software Corp."
#property link      "https://www.mql5.com"
#property version   "1.00"
#property strict

#include <OTFNContext.mqh>

input string StrategyName = "Trace";
input int Magic = 201505201;

OTFNContext *context;

//+------------------------------------------------------------------+
//| Expert initialization function                                   |
//+------------------------------------------------------------------+
int OnInit()
{
    EventSetTimer(1);
    
    context = new OTFNContext(StrategyName, Magic);
    
    return(INIT_SUCCEEDED);
}

void OnDeinit(const int reason)
{
    EventKillTimer();
    context.DoOnDeinit();
}

void OnTick()
{
    context.DoOnTick();
}
//+------------------------------------------------------------------+
//| Timer function                                                   |
//+------------------------------------------------------------------+
void OnTimer()
{
    context.DoOnTimer();
}
//+------------------------------------------------------------------+
//| Tester function                                                  |
//+------------------------------------------------------------------+
double OnTester()
{
    return context.DoOnTester();
}
//+------------------------------------------------------------------+
