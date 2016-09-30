using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Util;
using Android.Telephony;

namespace MoneyLogger.Droid
{
	public class Communicator : Service
	{
		SMSReceiver receiver;		// 문자 수신기
		IntentFilter intentFilter;	

		public override void OnCreate()
		{
			base.OnCreate();
			
			// 현 객체가 실행함을 로그에 기록한다.
			Log.Info("Communicator", "Communicator started");

			// 문자를 수신할 수 있도록
			receiver = new SMSReceiver();				// 문자 수신기를 만들고
			intentFilter = new IntentFilter("android.provider.Telephony.SMS_RECEIVED");	// 문자 수신을 한다고 알려주고
			intentFilter.Priority = int.MaxValue;		// 우선순위를 최상으로 설정한 뒤
			RegisterReceiver(receiver, intentFilter);	// 문자를 수신한다고 등록한다.

			// 문자를 수신하는 다른 앱들의 리스트를 로그에 기록한다.
			Intent intent = new Intent("android.provider.Telephony.SMS_RECEIVED");
			var infos = PackageManager.QueryBroadcastReceivers(intent, 0);
			foreach (ResolveInfo info in infos)
			{
				Log.Info("Communicator", "Receiver name:" + info.ActivityInfo.Name + "; priority=" + info.Priority);
			}
		}

		public override void OnDestroy()
		{
			base.OnDestroy();

			// 서비스 종료시 문자 수신을 종료한다.
			UnregisterReceiver(receiver);
		}

		public override IBinder OnBind(Intent intent)
		{
			return null;
		}

		[BroadcastReceiver(Enabled = true, Label = "SMS Receiver")]
		[IntentFilter(new[] { "android.provider.Telephony.SMS_RECEIVED" })]
		private class SMSReceiver : BroadcastReceiver
		{
			public override void OnReceive(Context context, Intent intent)
			{
				var messegeBuilder = new StringBuilder();
				
				// 함수가 호출되면 작업에 대한 로그를 남기고
				Log.Info("SMSBroadcastReceiver", "Intent received: " + intent.Action);
				// 메세지 수신에 대한 작업이 아닐 시 함수를 종료한다.
				if (intent.Action != "android.provider.Telephony.SMS_RECEIVED") return;

				// 작업으로부터 정보를 가져와 저장한 뒤,
				Bundle bundle = intent.Extras;
				// 정보가 없으면 함수를 종료한다.
				if (bundle == null) return;

				// 데이터를 PDU(프로토콜 데이터 유닛)로 받는다
				var pdus = JNIEnv.GetArray<Java.Lang.Object>(bundle.Get("pdus").Handle);
				
				// 데이터를 문자메세지를 담기 위해 준비한다.
				SmsMessage[] msgs = new SmsMessage[pdus.Length];

				// 어짜피 Length가 1이라 한 번만 돈다.
				for (var i = 0; i < msgs.Length; i++)
				{
					// 데이터를 문자 메시지로 바꿔 저장한 뒤
					msgs[i] = SmsMessage.CreateFromPdu((byte[])pdus[i], intent.GetStringExtra("format"));
					
					// 문자 메시지로부터 송신자 번호와 문자 메시지 내용을 알아낸다.
					messegeBuilder.AppendLine($"SMS From: {msgs[i].OriginatingAddress}");
					messegeBuilder.AppendLine($"Body: {msgs[i].MessageBody}");
				}

				// 토스트로 출력한다.
				string message = messegeBuilder.ToString();
				Toast.MakeText(context, message, ToastLength.Short).Show();
			}
		}
	}
}