public class tk2dDemoDataDrivenCollection : tk2dDataDefinition {
	public override void Define() {
		// These paths are relative to the project root
		// All subsequent paths are stacked on top of the parent
		Settings.InputPath = "TK2DROOT/tk2d_demo/datadrivencollection";
		Settings.OutputPath = "Unversioned/datadrivencollection";
		Settings.PixelsPerMeter = 10;

		SpriteCollection.Add( "DataDrivenTest1" );
			SpriteCollection.InputPath = "textures"; // Appends this texture path to Settings.inputPath for this sprite collection only
			Sprite.Add("anim/anim*.png").Anchor("LowerLeft");

		SpriteCollection.Add( "PlatformTest" ).Platforms("1x", "2x"); // the default is the first platform
			SpriteCollection.InputPath = "textures/platform";
			Sprite.Add("sample@1x.png");

		// Add animations
		DefineTrigger("MyTriggerName", "FootStep", 0, 3.0f); // subsequently these triggers are referenced by name. valid in all animation collections.

		AnimationCollection.Add( "DataDrivenAnim1" );
			AnimationCollection.DefaultFps = 30;
			
			Clip.Add("Sample1", "Once", 15);
				Frame.Add("ani*").Reverse();

			Clip.Add("Sample2", "Loop");
				Frame.Add("anim*").Range(4, 1);
				Frame.Add("DataDrivenTest1:anim*").Range(1, 4);

			Clip.Add("Platform", "Once");
				Frame.Add("sample");
				Frame.Add("anim2").Trigger("MyTriggerName");
	}
}


