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
		SMSReceiver receiver;		// ���� ���ű�
		IntentFilter intentFilter;	

		public override void OnCreate()
		{
			base.OnCreate();
			
			// �� ��ü�� �������� �α׿� ����Ѵ�.
			Log.Info("Communicator", "Communicator started");

			// ���ڸ� ������ �� �ֵ���
			receiver = new SMSReceiver();				// ���� ���ű⸦ �����
			intentFilter = new IntentFilter("android.provider.Telephony.SMS_RECEIVED");	// ���� ������ �Ѵٰ� �˷��ְ�
			intentFilter.Priority = int.MaxValue;		// �켱������ �ֻ����� ������ ��
			RegisterReceiver(receiver, intentFilter);	// ���ڸ� �����Ѵٰ� ����Ѵ�.

			// ���ڸ� �����ϴ� �ٸ� �۵��� ����Ʈ�� �α׿� ����Ѵ�.
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

			// ���� ����� ���� ������ �����Ѵ�.
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
				
				// �Լ��� ȣ��Ǹ� �۾��� ���� �α׸� �����
				Log.Info("SMSBroadcastReceiver", "Intent received: " + intent.Action);
				// �޼��� ���ſ� ���� �۾��� �ƴ� �� �Լ��� �����Ѵ�.
				if (intent.Action != "android.provider.Telephony.SMS_RECEIVED") return;

				// �۾����κ��� ������ ������ ������ ��,
				Bundle bundle = intent.Extras;
				// ������ ������ �Լ��� �����Ѵ�.
				if (bundle == null) return;

				// �����͸� PDU(�������� ������ ����)�� �޴´�
				var pdus = JNIEnv.GetArray<Java.Lang.Object>(bundle.Get("pdus").Handle);
				
				// �����͸� ���ڸ޼����� ��� ���� �غ��Ѵ�.
				SmsMessage[] msgs = new SmsMessage[pdus.Length];

				// ��¥�� Length�� 1�̶� �� ���� ����.
				for (var i = 0; i < msgs.Length; i++)
				{
					// �����͸� ���� �޽����� �ٲ� ������ ��
					msgs[i] = SmsMessage.CreateFromPdu((byte[])pdus[i], intent.GetStringExtra("format"));
					
					// ���� �޽����κ��� �۽��� ��ȣ�� ���� �޽��� ������ �˾Ƴ���.
					messegeBuilder.AppendLine($"SMS From: {msgs[i].OriginatingAddress}");
					messegeBuilder.AppendLine($"Body: {msgs[i].MessageBody}");
				}

				// �佺Ʈ�� ����Ѵ�.
				string message = messegeBuilder.ToString();
				Toast.MakeText(context, message, ToastLength.Short).Show();
			}
		}
	}
}