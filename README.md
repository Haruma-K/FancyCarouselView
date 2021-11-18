<h1 align="center">Fancy Carousel View</h1>

[![license](https://img.shields.io/badge/LICENSE-MIT-green.svg)](LICENSE.md)

[日本語ドキュメント(Japanese Documents Available)](README_JA.md)

Carousel View for Unity uGUI using <a href="https://github.com/setchi/FancyScrollView">Fancy Scroll View</a>.

<p align="center">
  <img width=700 src="https://user-images.githubusercontent.com/47441314/136406607-a3bc489f-2c77-40bc-bc6d-d2858f82a73c.gif" alt="Demo">
</p>

## Table of Contents
<!-- START doctoc generated TOC please keep comment here to allow auto update -->
<!-- DON'T EDIT THIS SECTION, INSTEAD RE-RUN doctoc TO UPDATE -->
<!-- param::isNotitle::true:: -->

- [Features](#features)
- [Demo](#demo)
- [Setup](#setup)
    - [Requirement](#requirement)
    - [Install](#install)
- [Basic Usage](#basic-usage)
    - [Create data for the cells](#create-data-for-the-cells)
    - [Create the cell view](#create-the-cell-view)
    - [Create the carousel view](#create-the-carousel-view)
    - [Initialize the carousel view](#initialize-the-carousel-view)
- [Advanced Usage](#advanced-usage)
    - [Understand the properties of the Carousel View](#understand-the-properties-of-the-carousel-view)
    - [Use the progress View](#use-the-progress-view)
    - [Custom cell movements](#custom-cell-movements)
    - [Use with scroll view](#use-with-scroll-view)
- [License](#license)

<!-- END doctoc generated TOC please keep comment here to allow auto update -->

## Features
* You can create the Carousel View in a few simple steps.  
* High performance because cells that are not needed for display are reused.
* Supports vertical and horizontal scrolling.
* The movement of the carousel and each parameters can be customized in detail.

## Demo
1. Clone this repository.
2. Open and play the following scene.
    * https://github.com/Haruma-K/FancyCarouselView/blob/master/Assets/Demo/Scenes/CarouselDemo.unity

## Setup

#### Requirement
Unity 2019.4 or higher.

#### Install
The Fancy Carousel View uses the <a href="https://github.com/setchi/FancyScrollView">Fancy Scroll View</a> as a low layer implementation.  
So you need to install both of them.

1. Open the Package Manager from Window > Package Manager
2. "+" button > Add package from git URL
3. Enter the following to install Fancy Scroll View
   * https://github.com/setchi/FancyScrollView.git#upm
4. Enter the following to install Fancy Carousel View
   * https://github.com/Haruma-K/FancyCarouselView.git?path=/Assets/FancyCarouselView

<p align="center">
  <img width=500 src="https://user-images.githubusercontent.com/47441314/118421190-97842b00-b6fb-11eb-9f94-4dc94e82367a.png" alt="Package Manager">
</p>

Or, open Packages/manifest.json and add the following to the dependencies block.

```json
{
    "dependencies": {
        "jp.setchi.fancyscrollview": "https://github.com/setchi/FancyScrollView.git#upm",
        "com.harumak.fancycarouselview": "https://github.com/Haruma-K/FancyCarouselView.git?path=/Assets/FancyCarouselView"
    }
}
```

If you want to set the target version, specify it like follow.

* https://github.com/Haruma-K/FancyCarouselView.git?path=/Assets/FancyCarouselView#1.0.0

## Basic Usage

#### Create data for the cells
First, create data for each of the cells that are elements of the carousel view.  
In the following example, defines the key to load cell background texture and the string that is displayed in the cell.

```cs
public class DemoData
{
    public string SpriteResourceKey { get; }
    public string Text { get; }

    public DemoData(string spriteResourceKey, string text)
    {
        SpriteResourceKey = spriteResourceKey;
        Text = text;
    }
}
```

#### Create the cell view
Next, create the view of the cell.  
As the following, inherit from the `CarouselCell` class and write the process of updating the view in the `Refresh` method.

```cs
using FancyCarouselView.Runtime.Scripts;
using UnityEngine;
using UnityEngine.UI;

public class DemoCarouselCell : CarouselCell<DemoData, DemoCarouselCell>
{
    [SerializeField] private Image _image = default;
    [SerializeField] private Text _text = default;

    protected override void Refresh(DemoData data)
    {
        _image.sprite = Resources.Load<Sprite>(data.SpriteResourceKey);
        _text.text = data.Text;
    }
}
```

Attach this script to a GameObject and create a prefab of the cell.

#### Create the carousel view
Next, create the carousel view.  
Create a class that inherits from the `CarouselView` class with the data and the cell class described above as the generic.

```cs
using FancyCarouselView.Runtime.Scripts;

public class DemoCarouselView : CarouselView<DemoData, DemoCarouselCell>
{
}
```

Attach it to a GameObject under Canvas.  
Adjust the size of the carousel view with the size of RectTransform, and adjust the size per cell with `Cell Size` property of `CarouselView`.  
Also, assign the prefab created in the previous section to the `Cell Prefab` property.

#### Initialize the carousel view
Finally, pass the data using the `CarouselView.Setup` method, and the carousel view will be displayed.

```cs
using System.Linq;
using UnityEngine;

namespace Demo.Scripts
{
    public class Demo : MonoBehaviour
    {
        [SerializeField] private DemoCarouselView _carouselView = default;
        [SerializeField, Range(1, 3)] private int _bannerCount = 3;

        private void Start()
        {
            var items = Enumerable.Range(0, _bannerCount)
                .Select(i =>
                {
                    var spriteResourceKey = $"tex_demo_banner_{i:D2}";
                    var text = $"Demo Banner {i:D2}";
                    return new DemoData(spriteResourceKey, text);
                })
                .ToArray();
            _carouselView.Setup(items);
        }
    }
}
```

## Advanced Usage

#### Understand the properties of the Carousel View
The description of each property in the Carousel View inspector is as follows.

<table>
<thead>
<tr><td colspan="3"><b>Property Name</b></td><td><b>Description</b></td></tr>
</thead>
<tbody>
<tr><td colspan="3">Cell Container</td><td>RectTransform representing the area of the carousel view.<br>Cells outside this area will be hidden and be reused.</td></tr>
<tr><td colspan="3">Cell Prefab</td><td>Prefab of the cell.</td></tr>
<tr><td colspan="3">Cell Size</td><td>Size of the cell.</td></tr>
<tr><td colspan="3">Cell Spacing</td><td>Spacing between cells.</td></tr>
<tr><td colspan="3">Snap Animation Durataion</td><td>Seconds of the snap animation.</td></tr>
<tr><td colspan="3">Snap Animation Type</td><td>Easing type of the snap animation.</td></tr>
<tr><td colspan="3">Auto Scrolling</td><td>If true, scroll automatically at regular intervals.</td></tr>
<tr><td></td><td colspan=2>Interval</td><td>Auto scrolling interval in seconds。</td></tr>
<tr><td></td><td colspan=2>Inverse Direction</td><td>If true, reverse the direction of the auto scrolling.</td></tr>
<tr><td colspan="3">Scroll Direction</td><td>Direction of the scrolling.</td></tr>
<tr><td colspan="3">Loop</td><td>If true, loop when reaches the end.</td></tr>
<tr><td colspan="3">Draggable</td><td>Dragable or not.</td></tr>
<tr><td colspan="3">Progress View</td><td>View that represents the progress. See below for details.</td></tr>
</tbody>
</table>

#### Use the progress View
Fancy Carousel View supports the progress view representing the carousel progress.

You can use the simple dot progress view by the following steps.

1. Instantiate `pfb_default_carousel_progress_view.prefab` in a scene.
2. Assign 1. to the Progress View property of the Carousel View.

If you want to change only the color or size of the dot while using `DotCarouselProgressView`, you can do so by replacing only the Progress Element Prefab in the Inspector of `DotCarouselProgressView`.

#### Custom cell movements
You can override `CarouselCell.OnPositionUpdated` to implement your own cell movement.  
The following is an example of a custom cell movements.

```cs
protected override void OnPositionUpdated(float position)
{
    base.OnPositionUpdated(position);

    var trans = transform;
    var pos = position * 2.0f - 1.0f;
    var absPos = Mathf.Abs(pos);
    var cellPosZ = Mathf.Lerp(0.0f, 120.0f, absPos);
    trans.localPosition = new Vector3(trans.localPosition.x, trans.localPosition.y, cellPosZ);
    trans.rotation = Quaternion.AngleAxis(pos * -20.0f, Vector3.up);
}
```

<p align="center">
  <img width=700 src="https://user-images.githubusercontent.com/47441314/136646317-d2138797-024a-44d4-af4c-b3d389972890.gif" alt="Demo">
</p>

#### Use with scroll view
When you use the carousel view as the content of the scroll view, the carousel view blocks the dragging of the scroll view according to the Unity specification.  
In other words, dragging the carousel view will not scroll the scroll view.

<p align="center">
  <img width=600 src="https://user-images.githubusercontent.com/47441314/139780467-5678bf8a-fe4b-46d4-a8e6-34c66c24d4f2.gif" alt="Demo">
</p>

In such a case, attach the `Scroll Event Propagator` component to the Carousel View GameObject.  
This component will propagate drag events to the parent `ScrollRect` properly.  
As a result, the scroll view and carousel view will work properly as shown below.

<p align="center">
  <img width=600 src="https://user-images.githubusercontent.com/47441314/139779762-13e992e1-ccc6-4819-a283-9ec5a79ce4e9.gif" alt="Demo">
</p>

## License
This software is released under the MIT License.  
You are free to use it within the scope of the license.  
However, the following copyright and license notices are required for use.

* [LICENSE.md](LICENSE.md)

And this software is implemented on the assumption that the following software is installed (not included).

* [FancyScrollView](https://github.com/setchi/FancyScrollView)

See [Third Party Notices.md](ThirdPartyNotices.md) for details on the license of this software.
