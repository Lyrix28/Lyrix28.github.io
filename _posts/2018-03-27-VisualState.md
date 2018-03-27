---
layout: post
title:  "Amazing VisualState"
date:   2018-03-27 14:06:05
categories: UWP
excerpt: trap
---

* content
{:toc}

## Amazing VisualState

{% highlight xaml linenos %}
<Page>
    <Grid>
        <VisualStateManager.VisualStateGroups>
        ···
        ···
        ···
        </VisualStateManager.VisualStateGroups>
    </Grid>
</Page>
{% endhighlight %}
>it works

{% highlight xaml linenos %}
<Page>
    <Grid>
        <Grid>
            <VisualStateManager.VisualStateGroups>
            ···
            ···
            ···
            </VisualStateManager.VisualStateGroups>
        </Grid>
    </Grid>
</Page>
{% endhighlight %}
>it fails