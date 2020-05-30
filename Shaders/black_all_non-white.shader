shader_type canvas_item;

void fragment()
{
	vec4 screen_c = texture(SCREEN_TEXTURE, SCREEN_UV);
	if (screen_c.rgb != vec3(1, 1, 1))
	{
		COLOR = vec4(0, 0, 0, screen_c.a);
	}
	else
	{
		COLOR = screen_c;
	}
}