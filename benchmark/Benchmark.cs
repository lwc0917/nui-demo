/*
 * Copyright (c) 2020 Samsung Electronics Co., Ltd.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *
 */

using System;
using System.Collections.Generic;
using Tizen.NUI;
using Tizen.NUI.UIComponents;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Constants;

class HelloWorldExample : NUIApplication
{
    public string IMG_DIR_PATH = System.Environment.CurrentDirectory;
    string[] IMAGE_PATH = new string[] {
        "/res/images/gallery-medium-1.jpg",
        "/res/images/gallery-medium-2.jpg",
        "/res/images/gallery-medium-3.jpg",
        "/res/images/gallery-medium-4.jpg",
        "/res/images/gallery-medium-5.jpg",
        "/res/images/gallery-medium-6.jpg",
        "/res/images/gallery-medium-7.jpg",
        "/res/images/gallery-medium-8.jpg",
        "/res/images/gallery-medium-9.jpg",
        "/res/images/gallery-medium-10.jpg",
        "/res/images/gallery-medium-11.jpg",
        "/res/images/gallery-medium-12.jpg",
        "/res/images/gallery-medium-13.jpg",
        "/res/images/gallery-medium-14.jpg",
        "/res/images/gallery-medium-15.jpg",
        "/res/images/gallery-medium-16.jpg",
        "/res/images/gallery-medium-17.jpg",
        "/res/images/gallery-medium-18.jpg",
        "/res/images/gallery-medium-19.jpg",
        "/res/images/gallery-medium-20.jpg",
        "/res/images/gallery-medium-21.jpg",
        "/res/images/gallery-medium-22.jpg",
        "/res/images/gallery-medium-23.jpg",
        "/res/images/gallery-medium-24.jpg",
        "/res/images/gallery-medium-25.jpg",
        "/res/images/gallery-medium-26.jpg",
        "/res/images/gallery-medium-27.jpg",
        "/res/images/gallery-medium-28.jpg",
        "/res/images/gallery-medium-29.jpg",
        "/res/images/gallery-medium-30.jpg",
        "/res/images/gallery-medium-31.jpg",
        "/res/images/gallery-medium-32.jpg",
        "/res/images/gallery-medium-33.jpg",
        "/res/images/gallery-medium-34.jpg",
        "/res/images/gallery-medium-35.jpg",
        "/res/images/gallery-medium-36.jpg",
        "/res/images/gallery-medium-37.jpg",
        "/res/images/gallery-medium-38.jpg",
        "/res/images/gallery-medium-39.jpg",
        "/res/images/gallery-medium-40.jpg",
        "/res/images/gallery-medium-41.jpg",
        "/res/images/gallery-medium-42.jpg",
        "/res/images/gallery-medium-43.jpg",
        "/res/images/gallery-medium-44.jpg",
        "/res/images/gallery-medium-45.jpg",
        "/res/images/gallery-medium-46.jpg",
        "/res/images/gallery-medium-47.jpg",
        "/res/images/gallery-medium-48.jpg",
        "/res/images/gallery-medium-49.jpg",
        "/res/images/gallery-medium-50.jpg",
        "/res/images/gallery-medium-51.jpg",
        "/res/images/gallery-medium-52.jpg",
        "/res/images/gallery-medium-53.jpg"
    };

	private int mRowsPerPage = 25;
	private int mColumnsPerPage = 25;
	private int mPageCount = 13;
    private int NUM_OF_IMG = 53;
	private Animation mShow;
	private Animation mScroll;
	private Animation mHide;
    private List<ImageView> mImageView = new List<ImageView>();
    private Vector3 mSize;

    /// <summary>
    /// Override to create the required scene
    /// </summary>
    protected override void OnCreate()
    {
        // Up call to the Base class first
        base.OnCreate();

        // Get the window instance and change background color
        var window = Window.Instance;
        window.BackgroundColor = Color.White;

        mSize = new Vector3(window.Size.Width / (float)mColumnsPerPage, window.Size.Height / (float)mRowsPerPage, 0.0f);

        CreateImageViews();
        ShowAnimation();

        window.KeyEvent += OnKeyEvent;
    }

    /// <summary>
    /// Called when any key event is received.
    /// Will use this to exit the application if the Back or Escape key is pressed
    /// </summary>
    private void OnKeyEvent( object sender, Window.KeyEventArgs eventArgs )
    {
        if( eventArgs.Key.State == Key.StateType.Down )
        {
            switch( eventArgs.Key.KeyPressedName )
            {
                case "Escape":
                case "Back":
                    Exit();
                break;
            }
        }
    }

    void CreateImageViews()
    {
        int actorCount = mRowsPerPage * mColumnsPerPage * mPageCount;

        for(int i = 0; i < actorCount; ++i)
        {
            ImageView tempImage = new ImageView()
            {
                ResourceUrl = IMG_DIR_PATH + IMAGE_PATH[i % NUM_OF_IMG],
                Size = new Vector3(0.0f, 0.0f, 0.0f),
                HeightResizePolicy = ResizePolicyType.Fixed,
                WidthResizePolicy = ResizePolicyType.Fixed,
            };

            mImageView.Add(tempImage);
            Window.Instance.Add(tempImage);
        }
    }

    public void ShowAnimation()
    {
        float xpos, ypos;
        int count = 0;
        int totalDuration = 10000;
        int durationPerActor = 500;
        int delayBetweenActors = (totalDuration - durationPerActor) / (mRowsPerPage * mColumnsPerPage);

        Vector3 initialPosition = new Vector3(Window.Instance.Size.Width * 0.5f, Window.Instance.Size.Height * 0.5f, 1000.0f);
        mShow = new Animation();

        int totalColumns = this.mColumnsPerPage * this.mPageCount;
        for(int i = 0; i < totalColumns; ++i)
        {
            xpos = mSize.X * i;
            for(int j = 0; j < mRowsPerPage; ++j)
            {
                ypos = mSize.Y * j;

                int delay = 0;
                int duration = 0;
                if(count < (mRowsPerPage * mColumnsPerPage))
                {
                    duration = durationPerActor;
                    delay    = delayBetweenActors * count;
                }

                mImageView[count].Position = initialPosition;
                mImageView[count].Size = new Vector3(0.0f, 0.0f, 0.0f);

                mShow.AnimateTo(mImageView[count], "Position", new Vector3(xpos, ypos, 0.0f), delay, delay + duration, new AlphaFunction(AlphaFunction.BuiltinFunctions.EaseOutBack));
                mShow.AnimateTo(mImageView[count], "Size", new Vector3(mSize.X, mSize.Y, 0.0f), delay, delay + duration, new AlphaFunction(AlphaFunction.BuiltinFunctions.EaseOutBack));

                ++count;
            }
        }

        mShow.Play();
        mShow.Finished += AnimationFinished;
    }

    public void ScrollAnimation()
    {
        mScroll = new Animation();
        int actorCount = mRowsPerPage * mColumnsPerPage * mPageCount;

        for (int i = 0; i < actorCount; ++i)
        {
            mScroll.AnimateBy(mImageView[i], "Position", new Vector3(-4.0f * 480, 0, 0.0f), 0, 300, new AlphaFunction(AlphaFunction.BuiltinFunctions.EaseOut));
            mScroll.AnimateBy(mImageView[i], "Position", new Vector3(-4.0f * 480, 0, 0.0f), 300, 600, new AlphaFunction(AlphaFunction.BuiltinFunctions.EaseOut));
            mScroll.AnimateBy(mImageView[i], "Position", new Vector3(-4.0f * 480, 0, 0.0f), 600, 800, new AlphaFunction(AlphaFunction.BuiltinFunctions.EaseOut));
            mScroll.AnimateBy(mImageView[i], "Position", new Vector3(12.0f * 480, 0, 0.0f), 800, 1000, new AlphaFunction(AlphaFunction.BuiltinFunctions.EaseOut));
        }

        mScroll.Play();
        mScroll.Finished += AnimationFinished;
    }

     public void HideAnimation()
     {
        int count = 0;
        int totalDuration = 5000;
        int durationPerActor = 500;
        int delayBetweenActors = (totalDuration - durationPerActor) / (mRowsPerPage * mColumnsPerPage);
        int actorsPerPage = this.mRowsPerPage * this.mColumnsPerPage;
        int totalColumns = this.mColumnsPerPage * this.mPageCount;

        mHide = new Animation();

        for(int i = 0; i < mRowsPerPage; ++i)
        {
            for(int j = 0; j < totalColumns; ++j)
            {
                int delay = 0;
                int duration = 0;

                if(count < actorsPerPage)
                {
                    duration = durationPerActor;
                    delay    = delayBetweenActors * count;
                }

                mHide.AnimateTo(mImageView[count], "Orientation", new Rotation(new Radian(new Degree(70.0f)), PositionAxis.X), delay, delay + duration, new AlphaFunction(AlphaFunction.BuiltinFunctions.EaseOut));
                mHide.AnimateBy(mImageView[count], "PositionZ", 1600, delay + delayBetweenActors * (mRowsPerPage * mColumnsPerPage) + duration, delay + delayBetweenActors * (mRowsPerPage * mColumnsPerPage) + duration + duration, new AlphaFunction(AlphaFunction.BuiltinFunctions.EaseOutBack));

                count++;
             }

         }

         mHide.Play();
         mHide.Finished += AnimationFinished;
     }

    private void AnimationFinished(object source, EventArgs e)
    {
        if (source.Equals(mShow)) {
            this.ScrollAnimation();
        }
        else if (source.Equals(mScroll)) {
            this.HideAnimation();
        }
        else {
            Exit();
        }
    }


    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread] // Forces app to use one thread to access NUI
    static void Main(string[] args)
    {
        HelloWorldExample example = new HelloWorldExample();

        foreach(string arg in args)
        {
            if (arg.IndexOf("r") > 0)
                example.mRowsPerPage = Int32.Parse(arg.Substring(2));
            else if (arg.IndexOf("c") > 0)
                example.mColumnsPerPage = Int32.Parse(arg.Substring(2));
            if (arg.IndexOf("p") > 0)
                example.mPageCount = Int32.Parse(arg.Substring(2));
        }

        example.Run(args);
    }
}
